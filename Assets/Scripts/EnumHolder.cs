using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NumericalResource
public enum ResourceType {
    Health,
    Energy
}

// Conversation
public enum EndAction {
    NONE,
    CHOICE,
    EVENT
}

// DialogueManager
public enum DialogueState {
    Idle,
    Busy,
    EndReached,
    WaitforChoice
}