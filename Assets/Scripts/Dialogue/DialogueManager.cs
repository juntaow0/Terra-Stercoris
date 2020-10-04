using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueUI))]
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public static event Action<string, string, Action> OnTrigger; // UI binding trigger
    public static event Action<Choice[], int> OnChoice; // Choice binding trigger
    public static event Action<int> OnEndEvent; // Event Trigger
    public static event Action OnSkip;

    public float characterPerSecond;
    public float buttonSpacing;
    public GameObject UIPrefab;
    public GameObject buttonPrefab;

    private int currentTrigger;
    private DialogueUI dialogueUI;
    private Queue<Sentence> sentences;
    private Conversation currentConversation;
    private DialogueState state;
    public static bool InConversation {get; private set;}

    private void Awake() {
        if (instance!=null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
        dialogueUI = GetComponent<DialogueUI>();
        sentences = new Queue<Sentence>();
        dialogueUI.InitializeUI(UIPrefab, buttonPrefab, characterPerSecond,buttonSpacing);
        InConversation = false;
        state = DialogueState.Idle;
    }

    public void LoadConversation(Conversation conversation, int triggerID) {
        sentences.Clear();
        currentConversation = conversation;
        currentTrigger = triggerID;
        foreach (Sentence s in conversation.sentences) {
            sentences.Enqueue(s);
        }
        if (conversation.endAction==EndAction.CHOICE) {
            OnChoice?.Invoke(conversation.choices, triggerID);
        }
        dialogueUI.toggleDialogueBox(true);
        state = DialogueState.Idle;
        InConversation = true;
        DisplaySentence();
    }

    private void RunEndAction() {
        switch (currentConversation.endAction) {
            case EndAction.CHOICE:
                dialogueUI.toggleChoices(true);
                state = DialogueState.WaitforChoice;
                break;
            default:
                dialogueUI.toggleDialogueBox(false);
                InConversation = false;
                OnEndEvent?.Invoke(currentTrigger);
                break;
        }
    }

    public void DisplaySentence() {
        switch (state) {        
            case DialogueState.Idle:
                Sentence s = sentences.Dequeue();
                string name = null;
                if (currentConversation.speakers.Length !=0) {
                    name = currentConversation.speakers[s.speakerIndex];
                }
                state = DialogueState.Busy;
                OnTrigger?.Invoke(s.sentence, name, () => {
                    if (sentences.Count < 1) {
                        state = DialogueState.EndReached;
                    } else {
                        state = DialogueState.Idle;
                    }
                });
                return;
            case DialogueState.Busy:
                OnSkip?.Invoke();
                return;
            case DialogueState.EndReached:
                RunEndAction();
                return;
            default:
                return;
        }
    }
}