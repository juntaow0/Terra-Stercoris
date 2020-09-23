using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour {

    [SerializeField] private string START_SCENE = "main";
    [SerializeField] private string menuScene = "MainMenu";
    [SerializeField] private Canvas canvas = null;
    private string currentScene;
    private float lastTimeScale;

    void Start() {
        currentScene = SceneManager.GetActiveScene().name;
        lastTimeScale = Time.timeScale;
        if(canvas == null) {
            canvas = GetComponent<Canvas>();
        }
    }

    void Update() {
        if(currentScene != menuScene && Input.GetButtonDown("Fire2")) {
            PauseGame();
        }
    }

    public void StartGame() {
        if(currentScene == menuScene) {
            SceneManager.LoadScene(START_SCENE);
        } else {
            // Resume game
            Time.timeScale = lastTimeScale;
            canvas.enabled = false;
        }
    }

    public void PauseGame() {
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        canvas.enabled = true;
        // Pause
    }
}
