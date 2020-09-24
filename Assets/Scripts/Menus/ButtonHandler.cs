using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor; // For viewing all scenes. Might want to remove later
using UnityEngine.SceneManagement;

// TODO: Potentially split all of these into their own scripts for each menu

public class ButtonHandler : MonoBehaviour {

    [SerializeField] private string START_SCENE = "main";
    [SerializeField] private string menuScene = "MainMenu";
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

        // Get all scenes and add to dropdown. May get removed later.
        List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();
        foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
            if(scene.enabled) optionDataList.Add(new TMP_Dropdown.OptionData(scene.path));
        }
        loadDropdown.ClearOptions();
        loadDropdown.AddOptions(optionDataList);
    }

    void Update() {
        if(currentScene != menuScene && Input.GetButtonDown("Fire2")) {
            PauseGame();
        }
    }

    public void StartGame() {
        // Start game if main menu
        if(currentScene == menuScene) {
            SceneManager.LoadScene(START_SCENE);
        } else { // Resume game otherwise
            canvas.enabled = false;
            Time.timeScale = lastTimeScale;
        }
    }

    public void PauseGame() {
        lastTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        canvas.enabled = true;
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
            SceneManager.LoadScene(menuScene);
        }
    }

    public void OpenMenu(GameObject menu) {
        currentMenu.SetActive(false);
        previousMenus.Push(currentMenu);
        currentMenu = menu;
        currentMenu.SetActive(true);
    }

    public void Load() {
        SceneManager.LoadScene(loadDropdown.captionText.text);
    }
}
