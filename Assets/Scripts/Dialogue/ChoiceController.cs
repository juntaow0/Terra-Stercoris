using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ChoiceController : MonoBehaviour
{
    private float buttonSpacing;
    private int existingButton;
    private List<Button> buttons;
    private List<RectTransform> buttonTransforms;
    private List<TextMeshProUGUI> buttonText;
    private GameObject buttonPrefab;
    private Canvas ChoiceBox;

    public void InitializeChoice(Canvas ChoiceCanvas, GameObject UIButton, float buttonSpacing) {
        buttonPrefab = UIButton;
        buttons = new List<Button>();
        buttonTransforms = new List<RectTransform>();
        buttonText = new List<TextMeshProUGUI>();
        ChoiceBox = ChoiceCanvas;
        this.buttonSpacing = buttonSpacing;
        existingButton = 0;
    }

    private void BindChoices(Choice[] choices, string key, int triggerID) {
        int buttonCount = choices.Length;
        if (buttonCount > existingButton) {
            GenerateButtons(buttonCount - existingButton);
        }
        for (int i = 0; i < buttonCount; i++) {
            int choiceNumber = i;
            Conversation c = choices[i].nextConversation;
            buttonText[i].text = choices[i].text;
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(() => {
                ChoiceTracker.Track(key,choiceNumber);
                DialogueManager.instance.SetupChoiceEvent(choiceNumber);
                DialogueManager.instance.LoadConversation(c, triggerID);
                ResetButtons(buttonCount);
            });
        }
        ArrangeButtons(buttonCount);
    }

    private void ArrangeButtons(int count) {
        float buttonHeight = buttons[0].GetComponent<RectTransform>().rect.height;
        float startPos = -220 + ((buttonHeight * count + buttonSpacing * (count - 1)) / 2) - (buttonHeight / 2);
        float offset = buttonSpacing + buttonHeight;
        for (int i = 0; i < count; i++) {
            buttonTransforms[i].anchoredPosition = new Vector2(0, startPos + i * offset * -1);
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
            buttonText.Add(button.transform.GetComponentInChildren<TextMeshProUGUI>());
        }
        existingButton += count;
    }

    private void OnDisable() {
        DialogueManager.OnBindChoice -= BindChoices;
    }

    private void OnEnable() {
        DialogueManager.OnBindChoice += BindChoices;
    }
}