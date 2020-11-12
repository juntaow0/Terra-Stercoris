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
    [SerializeField] private SpellUI spellUI;
    [SerializeField] private WeaponUI weaponUI;
    [SerializeField] private Notification notification;
    [SerializeField] private Image fadeImage;
    private bool HUDLock = false;
    private Canvas[] UICanvases;

    public static UIManager instance { get; private set; }

    private void Awake() {
        instance = this;
        UICanvases = UIRoot.GetComponentsInChildren<Canvas>();
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
        if (PlayerController.instance == null) {
            return;
        }
        CharacterController playerInfo = PlayerController.instance?.characterController;
        SpellController playerSpell = PlayerController.instance?.GetComponent<SpellController>();
        WeaponController playerWeapon = PlayerController.instance?.GetComponent<WeaponController>();
        if (powerBar != null) {
            powerBar.bindPowerBar(playerInfo?.energy);
        }
        if (healthBar != null) {
            healthBar.bindHealthBar(playerInfo?.health);
        }

        if (spellUI != null  && playerSpell!=null) {
            spellUI.bindSpellSlot(playerSpell);
        }

        if (weaponUI != null && playerWeapon!=null) {
            weaponUI.bindWeaponSlot(playerWeapon);
        }
    }

    void toggleUI(bool state) {
        if (TimelineController.InCutscene||DialogueManager.InConversation) {
            state = false;
        }
        foreach (Canvas c in UICanvases) {
            c.enabled = state;
        }
        if (!hud.activeInHierarchy) {
            toggleHUD(state);
        }
        if (HUDLock) {
            toggleHUD(!state);
        }
    }

    public void toggleHUD(bool visible) {
        hud.SetActive(visible);
    }

    public void toggleHUDStateLock(bool state) {
        HUDLock = state;
    }

    public void showTooltip(IInteractable obj) {
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
