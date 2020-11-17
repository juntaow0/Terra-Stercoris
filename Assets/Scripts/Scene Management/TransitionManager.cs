using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

    public float FadeTime = 0.5f;

    public static TransitionManager instance { get; private set; }
    private string ActiveSceneName = null;

    void Awake() {
        instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadScene(string scene) {
        UIManager.instance.FadeIn(FadeTime, ()=> {
            SceneManager.UnloadSceneAsync(ActiveSceneName);
            ActiveSceneName = scene;
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        });
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (ActiveSceneName == null) {
                ActiveSceneName = SceneManager.GetActiveScene().name;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(ActiveSceneName));
        SceneDataLoader.LoadStates();
        UIManager.instance.Bind();
        UIManager.instance.FadeOut(FadeTime, null);
        DialogueManager.instance.InitializeUI();
        AudioManager.instance.KillSong();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    IEnumerator WaitForSceneLoad(string scene, bool elementsActive) {
        yield return null;

        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        //asyncLoad.allowSceneActivation = false;

        //if(PlayerController.instance != null) PlayerController.instance.transform.position = Vector2.zero;

        

        //asyncLoad.allowSceneActivation = true;

        //while(!asyncLoad.isDone) {
        //    yield return null;
        //}

        /*
        if (!elementsActive) {
            hud.SetActive(false);
            PlayerController.instance.gameObject.SetActive(false);
        } else {
            hud.SetActive(true);
            PlayerController.instance.gameObject.SetActive(true);
        }
        */

        //yield return FadeFromBlack();
    }
    
}
