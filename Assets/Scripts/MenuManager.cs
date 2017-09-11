/**
File Created Sept 11th 2017 - File name = MenuManager.cs
Author: Gabriel Gaudreau
Project: ShootingRangeGame
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    Canvas menuCanvas, controlsCanvas, customCanvas;

    /// <summary>
    /// Start method, will set canvases to their respective state, enabled or disabled as well as create playerprefs values.
    /// </summary>
	void Start () {
        PlayerPrefs.SetFloat("roundTime", 120.0f);
        PlayerPrefs.SetInt("numShots", 100);
        controlsCanvas.enabled = false;
        customCanvas.enabled = false;
	}

    /// <summary>
    /// Starts the game, changes to game scene.
    /// </summary>
    public void OnClickStart() {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Goes to Custom canvas.
    /// </summary>
    public void OnClickGoToCustom() {
        menuCanvas.enabled = false;
        customCanvas.enabled = true;
    }

    /// <summary>
    /// Goes to Controls canvas.
    /// </summary>
    public void OnClickGoToControls() {
        menuCanvas.enabled = false;
        controlsCanvas.enabled = true;
    }

    /// <summary>
    /// Exits game.
    /// </summary>
    public void OnClickExit() {
        Application.Quit();
    }

    /// <summary>
    /// Goes back to menu canvas.
    /// </summary>
    public void OnClickBack() {
        if (controlsCanvas.enabled)
            controlsCanvas.enabled = false;
        else if (customCanvas.enabled)
            customCanvas.enabled = false;
        menuCanvas.enabled = true;
    }

    /// <summary>
    /// Sets playerprefs value of time to 60s.
    /// </summary>
    public void OnClickTimeOne() {
        PlayerPrefs.SetFloat("roundTime", 60.0f);
    }

    /// <summary>
    /// Sets playerprefs value of time to 120s.
    /// </summary>
    public void OnClickTimeTwo() {
        PlayerPrefs.SetFloat("roundTime", 120.0f);
    }

    /// <summary>
    /// Sets playerprefs value of time to 300s.
    /// </summary>
    public void OnClickTimeThree() {
        PlayerPrefs.SetFloat("roundTime", 300.0f);
    }

    /// <summary>
    /// Sets playerprefs value of shots to 50.
    /// </summary>
    public void OnClickShotsOne() {
        PlayerPrefs.SetInt("numShots", 50);
    }

    /// <summary>
    /// Sets playerprefs value of shots to 100.
    /// </summary>
    public void OnClickShotsTwo() {
        PlayerPrefs.SetInt("numShots", 100);
    }

    /// <summary>
    /// Sets playerprefs value of shots to 250.
    /// </summary>
    public void OnClickShotsThree() {
        PlayerPrefs.SetInt("numShots", 250);
    }
}

