using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject UIRoot;
    [SerializeField] private GameObject hud;
    [SerializeField] private Tooltip toolTip;
    [SerializeField] private Powerbar powerBar;
    [SerializeField] private Healthbar healthBar;
    [SerializeField] private Notification notification;
    [SerializeField] private Image fadeImage;
    public static UIManager instance { get; private set; }

    private void Awake() {
        instance = this;
    }

    private void OnEnable() {
        TimelineController.OnTimelineStatus += toggleUI;
        DialogueManager.OnDialogueStatus += toggleUI;
    }

    private void OnDisable() {
        TimelineController.OnTimelineStatus -= toggleUI;
        DialogueManager.OnDialogueStatus -= toggleUI;
    }

    private void OnDestroy() {
        OnDisable();
    }

    public void Bind() {
        CharacterController playerInfo = PlayerController.instance.characterController;
        if (powerBar != null) {
            powerBar.bindPowerBar(playerInfo.energy);
        }
            
        if (healthBar != null) {
            healthBar.bindHealthBar(playerInfo.health);
        }
    }

    void toggleUI(bool state) {
        UIRoot.SetActive(state);
    }

    public void toggleHUD(bool visible) {
        hud.SetActive(visible);
    }

    public void showTooltip(InteractableObject obj) {
        toolTip.Show(obj);
    }

    public void hdieTooltip() {
        toolTip.Hide();
    }

    public void FadeIn(float FadeTime, Action Callback) {
        StartCoroutine(FadeToBlack(FadeTime, Callback));
    }

    public void FadeOut(float FadeTime, Action Callback) {
        StartCoroutine(FadeFromBlack(FadeTime, Callback));
    }

    private IEnumerator FadeToBlack(float FadeTime, Action Callback) {
        float fadeAmount = 0f;

        fadeImage.gameObject.SetActive(true);

        float startTime = Time.realtimeSinceStartup;

        while (fadeAmount < FadeTime) {
            fadeAmount = Time.realtimeSinceStartup - startTime;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, fadeAmount / FadeTime);
            yield return null;
        }
        Callback?.Invoke();
    }

    private IEnumerator FadeFromBlack(float FadeTime, Action Callback) {
        float fadeAmount = 0f;

        float startTime = Time.realtimeSinceStartup;

        while (fadeAmount < FadeTime) {
            fadeAmount = Time.realtimeSinceStartup - startTime;
            fadeImage.color = Color.Lerp(Color.black, Color.clear, fadeAmount / FadeTime);
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
        Callback?.Invoke();
    }
}
