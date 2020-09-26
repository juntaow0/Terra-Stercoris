using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{

    public Conversation initialConversation;

    public void StartConversation() {
        DialogueManager.instance.LoadConversation(initialConversation);
    }

    public void NextSentence() {
        if(DialogueManager.InConversation) {
            DialogueManager.instance.DisplaySentence();
        }
    }

    void OnEnable() {
        InputManager.OnNextDialogue += NextSentence;
    }

    void OnDisable() {
        InputManager.OnNextDialogue -= NextSentence;
    }

    void OnDestroy() {
        InputManager.OnNextDialogue -= NextSentence;
    }
}
