using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

    [SerializeField] private Image fadeImage;
    public static TransitionManager instance {get; private set;}
    public float FadeTime = 0.5f;
    [SerializeField] private string startScene;
    [SerializeField] private GameObject[] persistentObjects = null;

    public static event Action OnSceneLoad;

    void Awake() {
        instance = this;

        if(persistentObjects != null) {
            foreach(GameObject obj in persistentObjects) {
                DontDestroyOnLoad(obj);
            }
        }
    }

    void Start() {
        if(SceneManager.GetActiveScene().name == "DefaultScene")
            SceneManager.LoadScene(startScene);

        StartCoroutine(FadeFromBlack());
    }

    public void LoadScene(string scene) {
        StartCoroutine(WaitForSceneLoad(scene));
    }

    IEnumerator FadeToBlack() {
        float fadeAmount = 0f;

        fadeImage.gameObject.SetActive(true);

        float startTime = Time.realtimeSinceStartup;

        while(fadeAmount < FadeTime) {
            fadeAmount = Time.realtimeSinceStartup - startTime;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, fadeAmount / FadeTime);
            yield return null;
        }
    }

    IEnumerator FadeFromBlack() {
        float fadeAmount = 0f;

        float startTime = Time.realtimeSinceStartup;

        while(fadeAmount < FadeTime) {
            fadeAmount = Time.realtimeSinceStartup - startTime;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, fadeAmount / FadeTime);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    IEnumerator WaitForSceneLoad(string scene) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        asyncLoad.allowSceneActivation = false;

        yield return FadeToBlack();

        if(PlayerManager.player != null) PlayerManager.player.transform.position = Vector2.zero;

        asyncLoad.allowSceneActivation = true;

        while(!asyncLoad.isDone) {
            yield return null;
        }

        OnSceneLoad?.Invoke();

        yield return FadeFromBlack();
    }
}
