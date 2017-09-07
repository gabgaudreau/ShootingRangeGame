/**
File Created May 5th 2017 - File name = NPC.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using System;
using UnityEngine;

public class NPC : MonoBehaviour, IShootable {
    private float hp, deadTimer, speed, nearRadius, currRotVel, currVel, maxRotAcc, maxRotVel, currAcc, maxFleeVel, firstTimer, eyeDMG, headDMG, coreDMG, bodyDMG;
    private CircularPathNode target = null;
    private Vector3 direction;
    private bool firstTarget, dead, firstDead;
    private MeshRenderer[] meshes;
    private ParticleSystem[] particleSystems;
    private Collider[] colliders;

    /// <summary>
    /// Initializes different variables to different values
    /// </summary>
    void Start() {
        hp = 200; 
        eyeDMG = 75;
        headDMG = coreDMG = 50;
        bodyDMG = 25;
        speed = 0.05f;
        nearRadius = 0.5f;
        maxRotVel = 1.5f;
        currAcc = 1f;
        maxFleeVel = 2.0f;
        maxRotAcc = 0.01f;
        firstTimer = 1.0f;
        firstTarget = true;
        firstDead = true;
        deadTimer = 25.0f;
        meshes = GetComponentsInChildren<MeshRenderer>();
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        colliders = GetComponentsInChildren<Collider>();
        Debug.Log(colliders.Length);
    }

    /// <summary>
    /// 
    /// </summary>
    private void HideMesh() {
        foreach (MeshRenderer m in meshes) {
            m.enabled = false;
        }
        foreach (ParticleSystem ps in particleSystems) {
            ps.Stop();
        }
        foreach (Collider c in colliders) {
            c.enabled = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void ShowMesh() {
        foreach (MeshRenderer m in meshes) {
            m.enabled = true;
        }
        foreach (ParticleSystem ps in particleSystems) {
            ps.Play();
        }
        foreach (Collider c in colliders) {
            c.enabled = true;
        }
    }
    
    /// <summary>
    /// Update function, runs every frame, finds the npc's target node and moves to that until
    /// when it is close enough to his target, he will find the next node in his path and move to that node.
    /// </summary>
    void Update() {
            //Timer for first node rotation by npc
            firstTimer -= Time.deltaTime;
            if (firstTimer < 0.0f) {
                firstTarget = false;
            }
            //only happens once, can't be run in start. try awake?
            if (target == null) {
                target = FindClosestNode();
            }
            //Move until close enough to target, then find next node
            Move(target.WorldPos, firstTarget);
            if (Vector3.Distance(target.WorldPos, transform.position) < nearRadius) {
                target = target.Next;
            }
        if(dead) {
            deadTimer -= Time.deltaTime;
            if (firstDead) {
                firstDead = false;
                HideMesh();
            }
            if (deadTimer < 0) {
                deadTimer = 25.0f;
                hp = 200;
                ShowMesh();
                firstDead = true;
                dead = false;
            }

        }
    }

    /// <summary>
    /// Method from the IShootable interface, called when the target is hit by a projectile.
    /// </summary>
    /// <param name="objectHit">name of the object hit.</param>
    void IShootable.GotShot(string objectHit) {
        /**
        based on 100hp
        eye - 75hp - 75pts
        head - 50hp - 50pts
        core - 50hp - 50pts
        body - 25hp - 25pts
        */
        if (!dead) {
            if (objectHit == "Eye") {
                hp -= eyeDMG;
                GameManager.gm.AddScore(eyeDMG);
            }
            else if (objectHit == "Head") {
                hp -= headDMG;
                GameManager.gm.AddScore(headDMG);
            }
            else if (objectHit == "PowerSourceCore") {
                hp -= coreDMG;
                GameManager.gm.AddScore(coreDMG);
            }
            else { //Body includes everything that isn't eye, head or core.
                hp -= bodyDMG;
                GameManager.gm.AddScore(bodyDMG);
            }
            if (hp <= 0) {
                dead = true;
            }
        }
    }

    /// <summary>
    /// Movement behavior, calculates rotation speed and lerps to the target rotation and moves the npc forward
    /// </summary>
    /// <param name="targetPos">The position of the next node in the path.</param>
    /// <param name="startNode">Bool to tell the method if the position passed in is the position of the first node.</param>
    private void Move(Vector3 targetPos, bool firstTime) {
        //Rotation calculations
        direction = (targetPos - transform.position).normalized;
        currRotVel = Mathf.Min(currRotVel + maxRotAcc, maxRotVel);
        currVel = Mathf.Min(currVel + currAcc, maxFleeVel);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, currRotVel * 5 * Time.deltaTime);
        //Forward movement
        if (firstTime) {
            // x / 75 is to slow down the speed of the npc, allowing the rotation to complete
            transform.position += transform.forward * speed / 75;
        }
        else {
            //basic speed.
            transform.position += transform.forward * speed;
        }
    }

    /// <summary>
    /// Finds the closest node to the current position of the NPC
    /// </summary>
    /// <returns>Returns a CircularPathNode</returns>
    CircularPathNode FindClosestNode() {
        //dummy variable to save closest node
        CircularPathNode closestNode = null;
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < CircularPath.instance.Nodes.Length; i++) {
            //check distance vs minDistance
            if (Vector3.Distance(CircularPath.instance.Nodes[i].WorldPos, transform.position) < minDistance) {
                minDistance = Vector3.Distance(CircularPath.instance.Nodes[i].WorldPos, transform.position);
                closestNode = CircularPath.instance.Nodes[i];
            }
        }
        //return next since the npc will always spawn on the same position as a node
        return closestNode.Next;
    }
}
