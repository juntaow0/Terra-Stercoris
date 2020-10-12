using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

    public float FadeTime = 0.5f;
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject hud;

    public static TransitionManager instance { get; private set; }

    void Awake() {
        instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadScene(string scene, bool elementsActive = true) {
        StartCoroutine(WaitForSceneLoad(scene, elementsActive));
    }

    public void SetHUDVisibility(bool visible) {
        hud.SetActive(visible);
    }

    public IEnumerator FadeToBlack() {
        float fadeAmount = 0f;

        fadeImage.gameObject.SetActive(true);

        float startTime = Time.realtimeSinceStartup;

        while(fadeAmount < FadeTime) {
            fadeAmount = Time.realtimeSinceStartup - startTime;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, fadeAmount / FadeTime);
            yield return null;
        }
    }

    public IEnumerator FadeFromBlack() {
        float fadeAmount = 0f;

        float startTime = Time.realtimeSinceStartup;

        while(fadeAmount < FadeTime) {
            fadeAmount = Time.realtimeSinceStartup - startTime;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, fadeAmount / FadeTime);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    IEnumerator WaitForSceneLoad(string scene, bool elementsActive) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        asyncLoad.allowSceneActivation = false;

        //if(PlayerController.instance != null) PlayerController.instance.transform.position = Vector2.zero;

        yield return FadeToBlack();

        asyncLoad.allowSceneActivation = true;

        while(!asyncLoad.isDone) {
            yield return null;
        }

        if (!elementsActive) {
            hud.SetActive(false);
            PlayerController.instance.gameObject.SetActive(false);
        } else {
            hud.SetActive(true);
            PlayerController.instance.gameObject.SetActive(true);
        }

        //yield return FadeFromBlack();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        StartCoroutine(FadeFromBlack());
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
