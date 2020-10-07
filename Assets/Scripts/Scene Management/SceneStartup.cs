using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStartup : MonoBehaviour {

    [SerializeField] private bool LoadHUD = true;

    void Start() {
        if (InputManager.instance == null) {
            AsyncOperation scene = SceneManager.LoadSceneAsync("DefaultScene", LoadSceneMode.Additive);
            scene.completed += LateStart;
            scene.completed += FadeIn;
        } else {
            LateStart();
        }
    }

    void LateStart(AsyncOperation temp = null) {
        TransitionManager.instance.SetHUDVisibility(LoadHUD);
    }

    void FadeIn(AsyncOperation temp) {
        StartCoroutine(TransitionManager.instance.FadeFromBlack());
        TransitionManager.instance.Bind();
    }
}
