/**
File Created June 26th 2017 - File name = ShootingTarget.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;

public class ShootingTarget : MonoBehaviour, IShootable {
    private float hp, maxDMG, middleDMG, middlePlusOneDMG, middlePlusTwoDMG, middlePlusThreeDMG, middlePlusFourDMG, middlePlusFiveDMG, deadTimer;
    private bool dead, first;
    private MeshRenderer[] meshes;

    /// <summary>
    /// Start function, initializes variables.
    /// </summary>
    void Start() {
        first = true;
        deadTimer = 25.0f;
        hp = 200;
        middleDMG = maxDMG = 100;
        middlePlusOneDMG = 85;
        middlePlusTwoDMG = 65;
        middlePlusThreeDMG = 50;
        middlePlusFourDMG = 30;
        middlePlusFiveDMG = 15;
        meshes = GetComponentsInChildren<MeshRenderer>();
    }

    /// <summary>
    /// This will hide all the meshes of the object.
    /// </summary>
    void Hide() {
        foreach (MeshRenderer m in meshes) {
            m.enabled = false;
        }
    }

    /// <summary>
    /// This will show all the meshes of the object.
    /// </summary>
    void Show() {
        foreach (MeshRenderer m in meshes) {
            m.enabled = true;
        }
    }

    /// <summary>
    /// Update function, checks wether the target is dead or alive and hides/shows the meshes accordingly, also keeps track of the death timer.
    /// </summary>
    void Update() {
        if(deadTimer < 0) { //Target is now alive again.
            dead = false;
            deadTimer = 25.0f;
            first = true;
            hp = 200;
            Show();
        }
        if (dead) { //Target is dead.
            deadTimer -= Time.deltaTime;
            if (first) {
                Hide();
                first = false;
            }
        }
    }

    /// <summary>
    /// Method from the IShootable interface, called when the target is hit by a projectile.
    /// Will assign damage and score based on the object hit by the bullet.
    /// </summary>
    /// <param name="objectHit">name of the object hit.</param>
    public void GotShot(string objectHit) {
        /*
        middleDMG = 100;
        middlePlusOneDMG = 85;
        middlePlusTwoDMG = 65;
        middlePlusThreeDMG = 50;
        middlePlusFourDMG = 30;
        middlePlusFiveDMG = 15;
        */
        if (!dead) {
            if (objectHit == "MiddlePlus5") {
                hp -= middlePlusFiveDMG;
                GameManager.gm.AddScore(middlePlusFiveDMG, maxDMG);
            }
            else if (objectHit == "MiddlePlus4") {
                hp -= middlePlusFourDMG;
                GameManager.gm.AddScore(middlePlusFourDMG, maxDMG);
            }
            else if (objectHit == "MiddlePlus3") {
                hp -= middlePlusThreeDMG;
                GameManager.gm.AddScore(middlePlusThreeDMG, maxDMG);
            }
            else if (objectHit == "MiddlePlus2") {
                hp -= middlePlusTwoDMG;
                GameManager.gm.AddScore(middlePlusTwoDMG, maxDMG);
            }
            else if (objectHit == "MiddlePlus1") {
                hp -= middlePlusOneDMG;
                GameManager.gm.AddScore(middlePlusOneDMG, maxDMG);
            }
            else if (objectHit == "Middle") {
                hp -= middleDMG;
                GameManager.gm.AddScore(middleDMG, maxDMG);
            }
            if (hp <= 0) {
                dead = true;
            }
        }
    }
}
