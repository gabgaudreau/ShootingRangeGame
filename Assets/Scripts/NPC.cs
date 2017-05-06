/**
File Created May 5th 2017 - File name = NPC.cs
Author: Gabriel Gaudreau
Project: The1v1Game
*/
using UnityEngine;

public class NPC : MonoBehaviour {
    private float hp; //maybe int?
    private CircularPathNode target = null;

	void Start () {
        hp = 200;
	}

    //what i want from movement: 
    //go from node to node endlessly
    //when you reach a new node (radius) put next node as target, rotate towards target while going towards target
    //no slowing down
	void Update () {
		if (target == null) {
            target = FindClosestNode();
            Debug.Log(target.name);
        }
	}

    CircularPathNode FindClosestNode() {
        CircularPathNode closestNode = null;
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < CircularPath.instance.Nodes.Length; i++) {
            if(Vector3.Distance(CircularPath.instance.Nodes[i].WorldPos, transform.position) < minDistance) {
                minDistance = Vector3.Distance(CircularPath.instance.Nodes[i].WorldPos, transform.position);
                closestNode = CircularPath.instance.Nodes[i];
            }
        }
        return closestNode;
    }
}
