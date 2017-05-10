/**
File Created May 5th 2017 - File name = CircularPathNode.cs
Author: Gabriel Gaudreau
Project: The1v1Game
*/
using UnityEngine;

/// <summary>
/// Node class for the circular path, contains previous, next and worldPosition 
/// </summary>
public class CircularPathNode : MonoBehaviour {

    private CircularPathNode prev, next;
    public CircularPathNode Previous {
        get { return prev; }
        set { prev = value; }
    }
    public CircularPathNode Next {
        get { return next; }
        set { next = value; }
    }

    private Vector3 worldPos;
    public Vector3 WorldPos {
            get { return worldPos; }
    }

    /// <summary>
    /// Initializes worldPos
    /// </summary>
    void Start() {
        worldPos = transform.position;
    }
}
