/**
File Created May 11th 2017 - File name = Training_Manager.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager gm;

    // 0 - 13 - 26
    [SerializeField]
    GameObject npcPrefab;
    private float playerScore, roundTimer, scoreAccuracyPercent, totalPossibleScore, hitAccuracyPercent;
    private int shotCounter, shotsLeft, shotsHit;
    private bool firstSpawn, scoreBoardUp, gameIsPaused;
    [SerializeField]
    Text scoreText, scorePerShotText, shotCounterText, shotsLeftText, scoreAccuracyText, hitAccuracyText, roundTimeText, canvasTitleText;
    [SerializeField]
    Canvas statsCanvas, pauseCanvas, gameOverCanvas;

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
    void Start() {
        shotsLeft = PlayerPrefs.GetInt("numShots");
        roundTimer = PlayerPrefs.GetFloat("roundTime");
        firstSpawn = true;
        Cursor.lockState = CursorLockMode.Locked;
        scoreText.text = string.Format("Score: {0:#}", playerScore);
        shotCounterText.text = string.Format("Shot Counter: {0:#}", shotCounter);
        scorePerShotText.text = string.Format("Avg Score per Shot: {0:#0.00}", playerScore);
        shotsLeftText.text = string.Format("AMMO: {0:#}", shotsLeft);
        scoreAccuracyText.text = string.Format("Score Accuracy: {0:#0.00}%", scoreAccuracyPercent);
        hitAccuracyText.text = string.Format("Hit Accuracy: {0:#0.00}%", hitAccuracyPercent);
        roundTimeText.text = string.Format("{0:#0} s", roundTimer);
        statsCanvas.enabled = false;
        pauseCanvas.enabled = false;
        gameOverCanvas.enabled = false;
    }

    /// <summary>
    /// Public method to add score. Also adds up accuracy %.
    /// </summary>
    /// <param name="x">score to be added</param>
    /// <param name="max">Max possible score from that shot</param>
    public void AddScore(float x, float max) {
        shotsHit++;
        totalPossibleScore += max;
        playerScore += x;
        UpdateAccuracy();
        scoreText.text = string.Format("Score: {0:#}", playerScore);
        scorePerShotText.text = string.Format("Avg Score per Shot: {0:#.00}", playerScore / shotCounter);
    }

    /// <summary>
    /// Returns the amount of shots left.
    /// </summary>
    /// <returns>Int</returns>
    public int GetShotsLeft() {
        return shotsLeft;
    }

    /// <summary>
    /// Update Accuracy every shot taken. Updates stats panel information as well.
    /// </summary>
    void UpdateAccuracy() {
        hitAccuracyPercent = ((float)shotsHit / (float)shotCounter) * 100;
        scoreAccuracyPercent = (playerScore / totalPossibleScore) * 100;
        scorePerShotText.text = string.Format("Avg Score per Shot: {0:#.00}", playerScore / shotCounter);
        scoreAccuracyText.text = string.Format("Score Accuracy: {0:#.00}%", scoreAccuracyPercent);
        hitAccuracyText.text = string.Format("Hit Accuracy: {0:#.00}%", hitAccuracyPercent);
    }

    /// <summary>
    /// public method to add to the shotcounter variable. Used with the singleton, updates text info as well.
    /// </summary>
    public void AddShotCounter() {
        shotCounter++;
        shotsLeft--;
        if(shotsLeft <= 0) {
            GameOver("Out of Bullets!");
        }
        shotCounterText.text = string.Format("Shot Counter: {0:#}", shotCounter);
        shotsLeftText.text = string.Format("AMMO: {0:#}", shotsLeft);
        UpdateAccuracy();
    }

    /// <summary>
    /// Returns the game pause status bool.
    /// </summary>
    /// <returns>returns a bool, if game is paused or not</returns>
    public bool IsGamePaused() {
        return gameIsPaused;
    }

    /// <summary>
    /// This method will be executed with the round timer runs out or the player has no more bullets.
    /// </summary>
    void GameOver(string s) {
        canvasTitleText.text = s;
        statsCanvas.enabled = true;
        gameOverCanvas.enabled = true;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Update method, on escape, the cursor is unlocked and this method will spawn the initial wave of target dummies.
    /// </summary>
    void Update() {
        if (!gameIsPaused) {
            roundTimer -= Time.deltaTime;
            if(roundTimer <= 0.0f) {
                GameOver("Out of Time!");
            }
            roundTimeText.text = string.Format("{0:#.0} s", roundTimer);
            if (Input.GetKeyDown(KeyCode.Escape)) {
                pauseCanvas.enabled = true;
                gameIsPaused = true;
                Time.timeScale = 0.0f;
                if (Cursor.lockState == CursorLockMode.Locked)
                    Cursor.lockState = CursorLockMode.None;
            }
            if (Input.GetKeyDown(KeyCode.Tab)) {
                if (!statsCanvas.enabled) {
                    statsCanvas.enabled = true;
                }
                else {
                    statsCanvas.enabled = false;
                }
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

    /// <summary>
    /// Pause menu function to resume game.
    /// </summary>
    public void OnClickResume() {
        pauseCanvas.enabled = false;
        Time.timeScale = 1.0f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Pause menu function to return to main menu. Saves highscore.
    /// </summary>
    public void OnClickBackToMenu() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Pause menu button function to exit game.
    /// </summary>
    public void OnClickExit() {
        Application.Quit();
    }
}

//TODO
//canvas in menu to show all time best of each stat regardless of mode: score/avg score per shot/score accuracy/hit accuracy

//THINGS I WANT TO CHANGE:
//crouch/sprint
//custom text box or something for custom game settings
//sensitivity setting??
//player position in editor
//movement considers mouse orientation when calculating forward direction.
//air strafing csgo style
//fix cursor and aim, always goes a bit topleft of where cursor is