using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sentence {
    public int speakerIndex;
    [TextArea(2, 5)]
    public string sentence;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "ScriptableObjects/Dialogue/Conversation")]
public class Conversation : ScriptableObject
{
    public int conversationID;
    public StandingPicture[] speakers;
    public Sentence[] sentences;
    public Conversation nextConversation;
    public Choices choices;
}
