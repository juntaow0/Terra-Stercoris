using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

    [SerializeField] private TMPro.TextMeshProUGUI textObject;
    [SerializeField] private Image backgroundPanel;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Color textColor;
    [SerializeField] private Color backgroundColor;
    [SerializeField] private float fadeTime = 0.2f;

    public static Notification instance {get; private set;}

    [SerializeField] private float fadeProgress = 0f;
    private int fadeDirection = 1;
    private bool isFading = false;
    private string queuedText;
    private string currentText;
    private bool targetVisibility = false;

    void Start() {
        if(textObject == null) textObject = GetComponent<TMPro.TextMeshProUGUI>();
        backgroundPanel.color = Color.clear;
        textObject.color = Color.clear;

        instance = this;
    }

    public void SetText(string text) {
        queuedText = text;
        
    }

    public void Show() {
        targetVisibility = true;
        Fade(1);
    }

    public void Hide() {
        targetVisibility = false;
        Fade(-1);
    }

    void Fade(int multiplier) {
        fadeDirection = multiplier;
        if(!isFading) {
            StartCoroutine(fade());
        }
    }

    private IEnumerator fade() {
        isFading = true;
        while(true) {

            if(currentText != queuedText) {
                if(fadeProgress > 0) {
                    fadeDirection = -1;
                } else {
                    currentText = queuedText;
                    textObject.text = currentText;
                    rectTransform.sizeDelta = textObject.GetPreferredValues(currentText) + new Vector2(8,8);
                    fadeDirection = 1;
                }   
            } else {
                if(targetVisibility) {
                    if(fadeProgress >= fadeTime) {
                        break;
                    }
                } else {
                    if(fadeProgress <= 0) {
                        break;
                    }
                }
            }

            fadeProgress += Time.deltaTime * fadeDirection;

            backgroundPanel.color = Color.Lerp(Color.clear, backgroundColor, fadeProgress / fadeTime);
            textObject.color = Color.Lerp(Color.clear, textColor, fadeProgress / fadeTime);

            yield return null;
        }
        fadeProgress = Mathf.Clamp(fadeProgress, 0, fadeTime);
        isFading = false;
    }
}
