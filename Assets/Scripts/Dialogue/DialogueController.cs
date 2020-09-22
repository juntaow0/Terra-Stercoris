using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Conversation initialConversation;
    public event Action OnDisplay;

    void Start(){
        DialogueManager.instance.LoadConversation(initialConversation);
    }

    public void NextSentence() {
        DialogueManager.instance.DisplaySentence();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            NextSentence();
        }
    }
}
