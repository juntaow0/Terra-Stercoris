using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour {

    [SerializeField] private TMPro.TextMeshProUGUI textObject;
    [SerializeField] private RectTransform rectTransform;

    void Start() {
        if(textObject == null) textObject = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void SetText(string text) {
        string updatedText = InputManager.instance.INTERACT.ToString() + ": " + text;
        textObject.text = updatedText;
        rectTransform.offsetMax = (textObject.GetPreferredValues(updatedText) + new Vector2(4,4)) * new Vector2(1,0);
    }
}
