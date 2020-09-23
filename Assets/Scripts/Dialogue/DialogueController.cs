using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public Conversation initialConversation;

    void Start(){
        DialogueManager.instance.LoadConversation(initialConversation);
    }

    public void NextSentence() {
        DialogueManager.instance.DisplaySentence();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            NextSentence();
        }
    }
}
