using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpChoiceManager : MonoBehaviour {

    [SerializeField] private Conversation helpConversation;
    [SerializeField] private Conversation runConversation;

    [SerializeField] private DialogueTrigger mutantDialogue;

    /*
    public void SetDialogue(bool isRunning) {
        if(isRunning) {
            mutantDialogue.initialConversation = runConversation;
        } else {
            mutantDialogue.initialConversation = helpConversation;
        }
    }
    */
}
