using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SpeakerPos {
    LEFT,
    RIGHT
}

public enum EndAction {
    NONE,
    CHOICE,
    CONVERSATION,
    EVENT 
}

[System.Serializable]
public struct Sentence {
    public int speakerIndex;
    public SpeakerPos speakerPos;
    [TextArea(2, 5)]
    public string sentence;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "ScriptableObjects/Dialogue/Conversation")]
public class Conversation : ScriptableObject
{
    public int conversationID;
    public EndAction endAction;
    public Choices choices;
    public Conversation nextConversation;
    public UnityEvent endEvent;
    public StandingPicture[] speakers;
    public Sentence[] sentences;
}
