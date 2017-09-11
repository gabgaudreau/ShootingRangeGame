﻿/**
File Created May 11th 2017 - File name = Training_Manager.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager gm;

    // 0 - 13 - 26
    [SerializeField]
    GameObject npcPrefab;
    private float playerScore, roundTimer;
    private int shotCounter, shotsLeft;
    private bool firstSpawn;
    [SerializeField]
    Text scoreText, scorePerShotText, shotCounterText, shotsLeftText;

    /// <summary>
    /// Initializes singleton in the event that it is null
    /// </summary>
    void Awake() {
        if (gm == null)
            gm = this;
    }
    
    /// <summary>
    /// Start method, initializes variables and locks cursor.
    /// </summary>
    void Start () {
        shotsLeft = PlayerPrefs.GetInt("numShots");
        roundTimer = PlayerPrefs.GetFloat("roundTime");
        firstSpawn = true;
        Cursor.lockState = CursorLockMode.Locked;
        scoreText.text = string.Format("Score: {0:#}", playerScore);
        shotCounterText.text = string.Format("Shot Counter: {0:#}", shotCounter);
        scorePerShotText.text = string.Format("Avg Score per Shot: {0:#0.00}", playerScore / shotCounter);
        shotsLeftText.text = string.Format("AMMO: {0:#}", shotsLeft);
    }

    /// <summary>
    /// public method to add score.
    /// </summary>
    /// <param name="x">x is a float value passed it.</param>
    public void AddScore(float x) {
        playerScore += x;
        scoreText.text = string.Format("Score: {0:#}", playerScore);
        scorePerShotText.text = string.Format("Avg Score per Shot: {0:#0.00}", playerScore / shotCounter);
    }

    /// <summary>
    /// Returns the amount of shots left.
    /// </summary>
    /// <returns>Int</returns>
    public int GetShotsLeft() {
        return shotsLeft;
    }

    /// <summary>
    /// public method to add to the shotcounter variable. Used with the singleton, updates text info as well.
    /// </summary>
    public void AddShotCounter() {
        shotCounter++;
        shotsLeft--;
        shotCounterText.text = string.Format("Shot Counter: {0:#}", shotCounter);
        scorePerShotText.text = string.Format("Avg Score per Shot: {0:#0.00}", playerScore / shotCounter);
        shotsLeftText.text = string.Format("AMMO: {0:#}", shotsLeft);
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

//TODO:
//come up with a real title
//pause game --> exit to main menu --> exit game
//high score
//canvas for round timer in gamescene

//THINGS I WANT TO CHANGE:
//custom text box or something for custom game settings
//sensitivity setting??
//player position in editor
//movement considers mouse orientation when calculating forward direction.
//air strafing csgo style