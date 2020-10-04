using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI: MonoBehaviour
{
    public float buttonSpacing;
    public float CharacterPerSecond;
    private GameObject buttonPrefab;
    private Canvas DialogueBox;
    private Canvas ChoiceBox;
    private Text DialogueText;
    private Text Speaker;
    private int existingButton;
    private List<Button> buttons;
    private List<RectTransform> buttonTransforms;
    private List<Text> buttonText;
    private WaitForSecondsRealtime characterSpeed;
    private bool skip;

    public void InitializeUI(GameObject UIPrefab, GameObject UIButton) {
        GameObject ui = Instantiate(UIPrefab);
        buttonPrefab = UIButton;
        buttons = new List<Button>();
        buttonTransforms = new List<RectTransform>();
        buttonText = new List<Text>();
        DialogueBox = ui.transform.GetChild(0).GetChild(0).GetComponent<Canvas>();
        ChoiceBox = ui.transform.GetChild(1).GetChild(0).GetComponent<Canvas>();
        Camera cam = FindObjectOfType<Camera>();
        DialogueBox.worldCamera = cam;
        DialogueBox.planeDistance = 5;
        ChoiceBox.worldCamera = cam;
        ChoiceBox.planeDistance = 5;
        DialogueBox.enabled = false;
        ChoiceBox.enabled = false;
        DialogueText = DialogueBox.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        Speaker = DialogueBox.transform.GetChild(0).GetChild(1).GetComponent<Text>();
        existingButton = 0;
        characterSpeed = new WaitForSecondsRealtime(1/CharacterPerSecond);
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
        Speaker.text = name + ":";
        StartCoroutine(TypeCharacters(sentence, onComplete));
    }

    private void SkipAnimation() {
        skip = true;
    }

    private void BindChoices(Choice[] choices) {
        int buttonCount = choices.Length;
        if (buttonCount > existingButton) {
            GenerateButtons(buttonCount- existingButton);
        }
        for (int i = 0; i < buttonCount; i++) {
            Conversation c = choices[i].nextConversation;
            buttonText[i].text = choices[i].text;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(()=> {
                DialogueManager.instance.LoadConversation(c);
                ResetButtons(buttonCount);
            });
        }
        ArrangeButtons(buttonCount);
    }

    private void ArrangeButtons(int count) {
        float buttonHeight = buttons[0].GetComponent<RectTransform>().rect.height;
        float startPos = -220 + ((buttonHeight * count + buttonSpacing * (count - 1))/2)- (buttonHeight/2);
        float offset = buttonSpacing + buttonHeight;
        for (int i = 0; i < count; i++) {
            buttonTransforms[i].anchoredPosition = new Vector2(0,startPos+i*offset*-1);
            buttons[i].gameObject.SetActive(true);
        }
    }

    private void ResetButtons(int count) {
        for (int i = 0; i < count; i++) {
            buttons[i].gameObject.SetActive(false);
        }
        ChoiceBox.enabled = false;
    }

    private void GenerateButtons(int count) {
        for (int i = 0; i < count; i++) {
            GameObject button = Instantiate(buttonPrefab, ChoiceBox.transform);
            button.SetActive(false);
            buttons.Add(button.GetComponent<Button>());
            buttonTransforms.Add(button.GetComponent<RectTransform>());
            buttonText.Add(button.transform.GetComponentInChildren<Text>());
        }
        existingButton += count;
    }

    private void OnDisable() {
        DialogueManager.OnTrigger -= ShowSentence;
        DialogueManager.OnChoice -= BindChoices;
        DialogueManager.OnSkip -= SkipAnimation;
    }

    private void OnEnable() {
        DialogueManager.OnTrigger += ShowSentence;
        DialogueManager.OnChoice += BindChoices;
        DialogueManager.OnSkip += SkipAnimation;
    }
}