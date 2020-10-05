using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Choices
[System.Serializable]
public struct Choice {
    [TextArea(2, 5)]
    public string text;
    public Conversation nextConversation;
}

[System.Serializable]
public struct Sentence {
    public int speakerIndex;
    [TextArea(2, 5)]
    public string sentence;
}

// For HUD Action Selector
[System.Serializable]
struct ActionBundle {
    public ActionTemplate slot;
    public UnityEvent action;
}