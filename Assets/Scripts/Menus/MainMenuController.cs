﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor; // For viewing all scenes. Might want to remove later
using UnityEngine.SceneManagement;

// TODO: Potentially split all of these into their own scripts for each menu

public class MainMenuController : MonoBehaviour {

    [SerializeField] private string START_SCENE = "Barracks and Briefing Room";
    [SerializeField] private string menuScene = "TempMainMenu";
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private GameObject creditsMenu = null;
    [SerializeField] private GameObject mainMenu = null;
    [SerializeField] private GameObject loadMenu = null;

    private GameObject currentMenu = null;
    private Stack<GameObject> previousMenus = new Stack<GameObject>();

    private string currentScene;
    private float lastTimeScale;

    [SerializeField] private TMP_Dropdown loadDropdown = null;

    void Awake() {
        creditsMenu.SetActive(false);
        loadMenu.SetActive(false);

        currentMenu = mainMenu;
    }

    void Start() {
        currentScene = SceneManager.GetActiveScene().name;
        lastTimeScale = Time.timeScale;
        if(canvas == null) {
            canvas = GetComponent<Canvas>();
        }

        List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();

        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i) {
            string name = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            optionDataList.Add(new TMP_Dropdown.OptionData(name));
        }

        loadDropdown.ClearOptions();
        loadDropdown.AddOptions(optionDataList);
    }

    void OnEnable() {
        InputManager.OnPause += PauseGame;
    }

    void OnDisable() {
        InputManager.OnPause -= PauseGame;
    }

    public void ResumeGame() {
        canvas.enabled = false;
        Time.timeScale = lastTimeScale;
        InputManager.OnPause -= ResumeGame;
        InputManager.OnPause += PauseGame;
    }

    public void PauseGame() {
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        canvas.enabled = true;
        InputManager.OnPause -= PauseGame;
        InputManager.OnPause += ResumeGame;
    }

    public void GoBack() {
        if(previousMenus.Count > 0) {
            currentMenu.SetActive(false);
            currentMenu = previousMenus.Pop();
            currentMenu.SetActive(true);
        }
    }

    public void Exit() {
        if(currentScene == menuScene) {
            // We might not want to include this
            Debug.Log("Exiting game!");
            Application.Quit();
        } else {
            // Reset timescale
            Time.timeScale = lastTimeScale;
            TransitionManager.instance.LoadScene(menuScene);
            ResetMenu();
            ResumeGame();
        }
    }

    public void OpenMenu(GameObject menu) {
        currentMenu.SetActive(false);
        previousMenus.Push(currentMenu);
        currentMenu = menu;
        currentMenu.SetActive(true);
    }

    public void Load() {
        TransitionManager.instance.LoadScene(loadDropdown.captionText.text);
        ResetMenu();
        ResumeGame();
    }

    public void ResetMenu() {
        if(currentMenu != mainMenu) {
            currentMenu.SetActive(false);
            previousMenus.Clear();
            currentMenu = mainMenu;
            mainMenu.SetActive(true);
        }
    }
}
