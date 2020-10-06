using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public int triggerID;
    public Conversation initialConversation;
    public UnityEvent OnDialogueEnd;
    public bool playOnAwake;

    private void Start() {
        if (playOnAwake) {
            Invoke("StartConversation", 0.5f);
        }
    }

    public void StartConversation() {
        DialogueManager.instance.LoadConversation(initialConversation, triggerID);
    }

    public void NextSentence() {
        if(DialogueManager.InConversation) {
            DialogueManager.instance.DisplaySentence();
        }
    }

    // always run after each conversation
    void OnEventTrigger(int id) {
        if (id != triggerID) {
            return;
        }
        OnDialogueEnd?.Invoke();
    }

    void OnEnable() {
        InputManager.OnNextDialogue += NextSentence;
        DialogueManager.OnEndEvent += OnEventTrigger;
    }

    void OnDisable() {
        InputManager.OnNextDialogue -= NextSentence;
        DialogueManager.OnEndEvent -= OnEventTrigger;
    }

    void OnDestroy() {
        InputManager.OnNextDialogue -= NextSentence;
        DialogueManager.OnEndEvent -= OnEventTrigger;
    }
}
