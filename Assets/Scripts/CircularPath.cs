/**
File Created May 5th 2017 - File name = CircularPath.cs
Author: Gabriel Gaudreau
Project: The1v1Game
*/
using UnityEngine;

public class CircularPath : MonoBehaviour {
    public static CircularPath instance; //Singleton design pattern
    [SerializeField]
    private CircularPathNode[] nodes;
    public CircularPathNode[] Nodes {
        get { return nodes; }
    }

    /// <summary>
    /// This method will create the circular path formed by the nodes placed in the scene, assigning to each their previous and next nodes.
    /// </summary>
    void CreateCircularPath() {
        for (int i = 0; i < nodes.Length; i++) {
            if (i == 0) { //Special case i = 0, first node
                nodes[i].Next = nodes[i + 1];
                nodes[i].Previous = nodes[9];
            }
            else if(i == nodes.Length - 1) { //Special case i = last node
                nodes[i].Next = nodes[0];
                nodes[i].Previous = nodes[i - 1];
            }
            else { //Every other case
                nodes[i].Next = nodes[i + 1];
                nodes[i].Previous = nodes[i - 1];
            }
        }
    }

    /// <summary>
    /// Checks for singleton not to be null
    /// </summary>
    void Start() {
        CreateCircularPath();
        if (instance == null)
            instance = this;
    }
}
