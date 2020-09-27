using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tooltip : MonoBehaviour {

    [SerializeField] private TMPro.TextMeshProUGUI textObject;

    void Start() {
        if(textObject == null) textObject = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void SetText(string text) {
        textObject.text = InputManager.instance.INTERACT.ToString() + ": " + text;
    }
}
