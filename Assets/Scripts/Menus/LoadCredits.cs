using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCredits : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI textObj = null;
    [SerializeField] private TextAsset textAsset = null;

    void Start() {
        textObj = GetComponent<TextMeshProUGUI>();
        textObj.text = textAsset.text;
    }
}
