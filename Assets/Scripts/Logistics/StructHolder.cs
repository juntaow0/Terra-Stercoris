using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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
    public FontStyles fontStyle;
    public TMP_FontAsset fontOverride;
    [TextArea(2, 5)]
    public string sentence;
}

// For HUD Action Selector
[System.Serializable]
public struct ActionBundle {
    public ActionTemplate slot;
    public UnityEvent action;
}

// For Dialogue Trigger
[System.Serializable]
public struct DialogueEndEvent {
    public UnityEvent[] events;
}