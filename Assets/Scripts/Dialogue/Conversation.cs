using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Session", menuName = "Dialogue/Session")]
public class Conversation : ScriptableObject
{
    [Header("Content")]
    public TMP_FontAsset fontOverride;
    public string[] speakers;
    public Sentence[] sentences;

    [Header("Settings")]
    public EndAction endAction;
    public Choice[] choices;
    public string choiceKey;
}
