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
    NORMAL,
    CHOICE,
}

// DialogueManager
public enum DialogueState {
    Idle,
    Busy,
    EndReached,
    WaitforChoice
}

// Weapon
public enum WeaponType {
        MELEE,
        RANGED
}

// Spell
public enum SpellStates {
    Ready,
    Chanting,
    Casting,
    Finished,
    Cooldown
}

// For Context based music
public enum MusicState {
    Normal,
    Combat
}