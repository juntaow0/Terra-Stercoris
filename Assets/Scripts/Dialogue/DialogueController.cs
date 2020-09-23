using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Conversation initialConversation;

    void Start(){
        StartConversation(); // temp
    }

    public void StartConversation() {
        DialogueManager.instance.LoadConversation(initialConversation);
    }

    public void NextSentence() {
        DialogueManager.instance.DisplaySentence();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0)) {
            NextSentence();
        }
    }
}
