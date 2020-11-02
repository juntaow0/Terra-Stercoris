using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ChoiceController))]
public class DialogueUI: MonoBehaviour
{
    private Canvas DialogueBox;
    private Canvas ChoiceBox;
    private TextMeshProUGUI DialogueText;
    private TextMeshProUGUI Speaker;
    private ChoiceController choiceController;
    private WaitForSecondsRealtime characterSpeed;
    private TMP_FontAsset defaultFont;
    private bool skip;
    private int soundMod = 0;

    public void InitializeUI(GameObject UIPrefab, GameObject UIButton, TMP_FontAsset fontAsset, float characterPerSecond, float buttonSpacing) {
        choiceController = GetComponent<ChoiceController>();
        GameObject ui = Instantiate(UIPrefab);
        DialogueBox = ui.transform.GetChild(0).GetChild(0).GetComponent<Canvas>();
        ChoiceBox = ui.transform.GetChild(1).GetChild(0).GetComponent<Canvas>();
        DialogueBox.enabled = false;
        ChoiceBox.enabled = false;
        choiceController.InitializeChoice(ChoiceBox,UIButton,buttonSpacing);
        
        DialogueText = DialogueBox.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        Speaker = DialogueBox.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        characterSpeed = new WaitForSecondsRealtime(1/characterPerSecond);
        defaultFont = DialogueText.font;
        if (fontAsset != null) {
            defaultFont = fontAsset;
        }
        skip = false;
    }
    public void toggleDialogueBox(bool state) {
        DialogueBox.enabled = state;
    }

    public void toggleChoices(bool state) {
        ChoiceBox.enabled = state;
    }
    public void SetFont(TMP_FontAsset font) {
        defaultFont = font;
    }

    IEnumerator TypeCharacters(Sentence sentencePack, Action onComplete) {
        DialogueText.text = "";
        if (DialogueText.font != defaultFont) {
            if (sentencePack.fontOverride != null) {
                DialogueText.font = sentencePack.fontOverride;
            } else {
                DialogueText.font = defaultFont;
            }
        }
        DialogueText.fontStyle = sentencePack.fontStyle;
        string sentence = sentencePack.sentence;
        foreach (char c in sentence) {
            if (skip) {
                DialogueText.text = sentence;
                skip = false;
                break;
            }
            DialogueText.text += c;
            if (soundMod % 4 == 0) {
                AudioManager.instance.Play("Talk");
                soundMod = 0;
            }
            soundMod++;
            yield return characterSpeed;
        }
        onComplete?.Invoke();
    }

    private void ShowSentence(Sentence sentence, string name, Action onComplete) {
        Speaker.text = "";
        if (name != "") {
            Speaker.text = name + ":";
        }
        StartCoroutine(TypeCharacters(sentence, onComplete));
    }

    private void SkipAnimation() {
        skip = true;
    }

    private void OnDisable() {
        DialogueManager.OnTrigger -= ShowSentence;
        DialogueManager.OnSkip -= SkipAnimation;
    }

    private void OnEnable() {
        DialogueManager.OnTrigger += ShowSentence;
        DialogueManager.OnSkip += SkipAnimation;
    }
}