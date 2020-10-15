﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStartup : MonoBehaviour {

    [SerializeField] private bool LoadHUD = true;
    public event Action OnSceneLoad;

    private void Awake() {
        if (!SceneManager.GetSceneByName("DefaultScene").isLoaded) {
            SceneManager.LoadSceneAsync("DefaultScene", LoadSceneMode.Additive).completed += OnSceneLoadCompleted;
        } else {
            UIManager.instance.toggleHUD(LoadHUD);
        }
            

        /*
        if (InputManager.instance == null) {
            AsyncOperation scene = SceneManager.LoadSceneAsync("DefaultScene", LoadSceneMode.Additive);
            scene.completed += LateStart;
            scene.completed += FadeIn;
        } else {
            LateStart();
        }
        */
    }
    void OnSceneLoadCompleted(AsyncOperation op) {
        UIManager.instance.toggleHUD(LoadHUD);
        OnSceneLoad?.Invoke();
    }

    /*
    void FadeIn(AsyncOperation temp) {
        StartCoroutine(TransitionManager.instance.FadeFromBlack());
        TransitionManager.instance.Bind();
    }
    */
}
