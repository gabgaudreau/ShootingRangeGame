/**
File Created May 11th 2017 - File name = Training_Manager.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gm;

    // 0 - 13 - 26
    [SerializeField]
    GameObject npcPrefab;
    private float playerScore;
    private int shotCounter;
    private bool firstSpawn;

    /// <summary>
    /// Initializes singleton in the event that it is null
    /// </summary>
    void Awake() {
        if (gm == null)
            gm = this;
    }

    /// <summary>
    /// public method to add score.
    /// </summary>
    /// <param name="x">x is a float value passed it.</param>
    public void AddScore(float x) {
        playerScore += x;
    }

    /// <summary>
    /// public method to add to the shotcounter variable. Used with the singleton
    /// </summary>
    public void AddShotCounter() {
        shotCounter++;
    }

    /// <summary>
    /// Start method, initializes variables and locks cursor.
    /// </summary>
    void Start () {
        firstSpawn = true;
        Cursor.lockState = CursorLockMode.Locked;
	}

    /// <summary>
    /// Update method, on escape, the cursor is unlocked and this method will spawn the initial wave of target dummies.
    /// </summary>
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
        }
        //can this be run in start?? using a bool to only execute once in update. This is the initial spawn of enemies.
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

//TODO:
//uh comments.
//make enemies die and respawn
//canvas score/shot/stuff

//BUGS:
//movement considers mouse orientation when calculating forward direction.
//no air strafing