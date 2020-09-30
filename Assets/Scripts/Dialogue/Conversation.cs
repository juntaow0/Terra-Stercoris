using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/Conversation")]
public class Conversation : ScriptableObject
{
    public int conversationID;
    public EndAction endAction;
    public Choices choices;
    public Conversation nextConversation;
    public UnityEvent endEvent;
    public string[] speakers;
    public Sentence[] sentences;
}
