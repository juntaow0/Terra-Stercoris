using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public int triggerID;
    
    public Conversation[] Conversations;
    public DialogueEndEvent[] EndEvents;
    private int defaultIndex = 0;
    private UnityEvent[] currentEvents;
    private UnityEvent currentEvent;

    public void StartConversation(int index) {
        LoadConversation(index);
    }

    public void StartRandomConversation() {
        int index = Random.Range(0, Conversations.Length);
        LoadConversation(index);
    }

    public void StartDefaultConversation() {
        LoadConversation(defaultIndex);
    }

    public void StartConversationByChoice(string key) {
        int choiceNumber = ChoiceTracker.GetChoiceNumber(key);
        LoadConversation(choiceNumber);
    }

    public void SetDefaultIndex(int index) {
        defaultIndex = index;
    }

    public void NextSentence() {
        DialogueManager.instance.DisplaySentence();
    }

    // always run after each conversation
    void OnEventTrigger(int id) {
        if (id != triggerID) {
            return;
        }
        InputManager.OnNextDialogue -= NextSentence;
        currentEvent?.Invoke();
    }

    void SwapEndEvent(int index) {
        if (currentEvents.Length > 0) {
            currentEvent = currentEvents[index];
        }
    }

    private void LoadConversation(int index) {
        if (EndEvents.Length > 0) {
            currentEvents = EndEvents[index].events;
            if (currentEvents.Length > 0) {
                currentEvent = currentEvents[0];
            }
        }
        InputManager.OnNextDialogue += NextSentence;
        DialogueManager.instance.LoadConversation(Conversations[index], triggerID);
    }

    void OnEnable() {
        DialogueManager.OnEndEvent += OnEventTrigger;
        DialogueManager.OnChoice += SwapEndEvent;
    }

    void OnDisable() {
        InputManager.OnNextDialogue -= NextSentence;
        DialogueManager.OnEndEvent -= OnEventTrigger;
        DialogueManager.OnChoice -= SwapEndEvent;
    }

    void OnDestroy() {
        OnDisable();
    }
}
