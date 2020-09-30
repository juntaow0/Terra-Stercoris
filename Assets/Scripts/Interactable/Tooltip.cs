using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    [SerializeField] private TMPro.TextMeshProUGUI textObject;
    [SerializeField] private Image backgroundPanel;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Color textColor;
    [SerializeField] private Color backgroundColor;
    [SerializeField] private float fadeTime = 0.2f;

    private InteractableObject targetObject = null;
    private InteractableObject currentObject = null;
    private float fadeProgress = 0f;

    void Start() {
        if(textObject == null) textObject = GetComponent<TMPro.TextMeshProUGUI>();
        backgroundPanel.color = Color.clear;
        textObject.color = Color.clear;
    }

    public void Show(InteractableObject newObject) {
        targetObject = newObject;
        if(currentObject == null || fadeProgress >= fadeTime) {
            StartCoroutine(fade());
        }
    }

    public void Hide() {
        Show(null);
    }

    private IEnumerator fade() {

        while(currentObject != null || targetObject != null) {
            if(fadeProgress <= 0) {
                currentObject = targetObject;
                if(targetObject != null) {
                    string newMessage = InputManager.instance.INTERACT.ToString() + ": " + targetObject.message;
                    textObject.text = newMessage;
                    rectTransform.offsetMax = (textObject.GetPreferredValues(newMessage) + new Vector2(4,4)) * new Vector2(1,0);
                    transform.position = targetObject.transform.position;
                }
            }

            if(currentObject != targetObject || targetObject == null) {
                if(fadeProgress > 0) {
                    fadeProgress -= Time.deltaTime;
                }
            } else if(fadeProgress < fadeTime) {
                fadeProgress += Time.deltaTime;
            }

            backgroundPanel.color = Color.Lerp(Color.clear, backgroundColor, fadeProgress / fadeTime);
            textObject.color = Color.Lerp(Color.clear, textColor, fadeProgress / fadeTime);

            if(fadeProgress >= fadeTime) {
                fadeProgress = fadeTime;
                break;
            }

            yield return null;
        }
    }
}
