using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Choice {
    [TextArea(2, 5)]
    public string text;
    public Conversation conversation;
}

[CreateAssetMenu(fileName = "New Choice", menuName = "Dialogue/Choice")]
public class Choices : ScriptableObject {
    public int ChoiceID;
    public Choice[] choices;
}
