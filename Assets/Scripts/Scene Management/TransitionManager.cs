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

    [SerializeField] public GameObject hud;
    private Canvas hudCanvas;
    [SerializeField] public GameObject menu;
    [SerializeField] public GameObject managers;

    [SerializeField] private List<GameObject> persistentObjects = new List<GameObject>();

    public static event Action OnSceneLoad;

    void Awake() {
        instance = this;
        persistentObjects.Add(hud);
        persistentObjects.Add(menu);
        persistentObjects.Add(managers);
        hudCanvas = hud.GetComponent<Canvas>();
        if(persistentObjects != null) {
            foreach(GameObject obj in persistentObjects) {
                DontDestroyOnLoad(obj);
            }
        }
    }

    void Start() {
        if(SceneManager.GetActiveScene().name == "DefaultScene")
            LoadScene(startScene);
    }

    public void LoadScene(string scene, bool elementsActive = true) {
        StartCoroutine(WaitForSceneLoad(scene, elementsActive));
    }

    public void SetHUDVisibility(bool visible) {
        hudCanvas.enabled = visible;
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

        yield return FadeToBlack();

        if(PlayerController.instance != null) PlayerController.instance.transform.position = Vector2.zero;

        asyncLoad.allowSceneActivation = true;

        while(!asyncLoad.isDone) {
            yield return null;
        }

        if(!elementsActive) {
            hud.SetActive(false);
            PlayerController.instance.gameObject.SetActive(false);
        } else {
            hud.SetActive(true);
            PlayerController.instance.gameObject.SetActive(true);
            Bind();
        }

        OnSceneLoad?.Invoke();

        yield return FadeFromBlack();
    }

    public void Bind() {
        PlayerController.instance.SetCamera(Camera.main);
        GameObject followCamera = GameObject.FindWithTag("FollowCamera");
        if(followCamera != null) {
            Cinemachine.CinemachineVirtualCamera virtualCamera = followCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            if(virtualCamera != null) virtualCamera.m_Follow = PlayerController.instance.transform;
        }
    }
}
