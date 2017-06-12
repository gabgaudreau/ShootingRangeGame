/**
File Created May 11th 2017 - File name = Training_Manager.cs
Author: Gabriel Gaudreau
Project: The1v1Game
*/
using UnityEngine;

public class TrainingManager : MonoBehaviour {
    // 0 - 13 - 26
    [SerializeField]
    GameObject npcPrefab;
    private float playerScore;
    private bool firstSpawn;

    void Start () {
        playerScore = 0;
        firstSpawn = true;
        Cursor.lockState = CursorLockMode.Locked;
	}

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
        }
        if (firstSpawn) {
            Instantiate(npcPrefab, CircularPath.instance.Nodes[0].WorldPos, Quaternion.identity);
            Instantiate(npcPrefab, CircularPath.instance.Nodes[13].WorldPos, Quaternion.identity);
            Instantiate(npcPrefab, CircularPath.instance.Nodes[26].WorldPos, Quaternion.identity);
            firstSpawn = false;
        }
	}
}
//Respawn Idea:
//Do something where the npc that get killed is never destroyed -> hide mesh -> instantiate body explosion -> wait some delay -> show mesh again
//this will keep the equal interval between npcs at all times.
