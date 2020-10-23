using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ChoiceController))]
public class DialogueUI: MonoBehaviour
{
    private Canvas DialogueBox;
    private Canvas ChoiceBox;
    private Text DialogueText;
    private Text Speaker;
    private ChoiceController choiceController;
    private WaitForSecondsRealtime characterSpeed;
    private bool skip;

    public void InitializeUI(GameObject UIPrefab, GameObject UIButton, float characterPerSecond, float buttonSpacing) {
        choiceController = GetComponent<ChoiceController>();
        GameObject ui = Instantiate(UIPrefab);
        DialogueBox = ui.transform.GetChild(0).GetChild(0).GetComponent<Canvas>();
        ChoiceBox = ui.transform.GetChild(1).GetChild(0).GetComponent<Canvas>();
        DialogueBox.enabled = false;
        ChoiceBox.enabled = false;
        choiceController.InitializeChoice(ChoiceBox,UIButton,buttonSpacing);

        DialogueText = DialogueBox.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        Speaker = DialogueBox.transform.GetChild(0).GetChild(1).GetComponent<Text>();
        characterSpeed = new WaitForSecondsRealtime(1/characterPerSecond);
        skip = false;
    }
    public void toggleDialogueBox(bool state) {
        DialogueBox.enabled = state;
    }

    public void toggleChoices(bool state) {
        ChoiceBox.enabled = state;
    }

    IEnumerator TypeCharacters(string sentence, Action onComplete) {
        DialogueText.text = "";
        foreach (char c in sentence) {
            if (skip) {
                DialogueText.text = sentence;
                skip = false;
                break;
            }
            DialogueText.text += c;
            yield return characterSpeed;
        }
        onComplete?.Invoke();
    }

    private void ShowSentence(string sentence, string name, Action onComplete) {
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