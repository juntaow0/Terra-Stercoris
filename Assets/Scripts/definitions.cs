using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NumericalResource
public enum ResourceType {
    Health,
    Energy
}

// Choices
[System.Serializable]
public struct Choice {
    [TextArea(2, 5)]
    public string text;
    public Conversation conversation;
}

// Weapon
public enum WeaponType {
        MELEE,
        RANGED
}

// Conversation
public enum EndAction {
    NONE,
    CHOICE,
    CONVERSATION,
    EVENT
}

[System.Serializable]
public struct Sentence {
    public int speakerIndex;
    [TextArea(2, 5)]
    public string sentence;
}

// DialogueManager
public enum DialogueState {
    Idle,
    Busy,
    EndReached,
    WaitforChoice
}