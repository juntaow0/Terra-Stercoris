using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(DialogueUI))]
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public static event Action<Sentence, string, Action> OnTrigger; // UI binding trigger
    public static event Action<Choice[], string, int> OnBindChoice; // Choice binding trigger
    public static event Action<int,int> OnChoice;
    public static event Action<int> OnEndEvent; // Event Trigger
    public static event Action OnSkip;
    public static event Action<bool> OnDialogueStatus;

    public float characterPerSecond;
    public float buttonSpacing;
    public GameObject UIPrefab;
    public GameObject buttonPrefab;
    public TMP_FontAsset fontAsset;

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
        InConversation = false;
        state = DialogueState.Idle;
    }

    public void InitializeUI() {
        dialogueUI.InitializeUI(UIPrefab, buttonPrefab, fontAsset, characterPerSecond,buttonSpacing);
    }

    public void LoadConversation(Conversation conversation, int triggerID) {
        sentences.Clear();
        currentConversation = conversation;
        currentTrigger = triggerID;
        foreach (Sentence s in conversation.sentences) {
            sentences.Enqueue(s);
        }
        if (conversation.endAction==EndAction.CHOICE) {
            OnBindChoice?.Invoke(conversation.choices, conversation.choiceKey, triggerID);
        }
        if (conversation.fontOverride != null) {
            dialogueUI.SetFont(conversation.fontOverride);
        }
        dialogueUI.toggleDialogueBox(true);
        OnDialogueStatus?.Invoke(false);
        state = DialogueState.Idle;
        InConversation = true;
        DisplaySentence();
    }

    public void SetupChoiceEvent(int choiceNumber){
        OnChoice?.Invoke(choiceNumber, currentTrigger);
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
                OnDialogueStatus?.Invoke(true);
                break;
        }
    }

    public void DisplaySentence() {
        if (InConversation) {
            switch (state) {
                case DialogueState.Idle:
                    Sentence s = sentences.Dequeue();
                    string name = "";
                    if (currentConversation.speakers.Length != 0) {
                        name = currentConversation.speakers[s.speakerIndex];
                    }
                    state = DialogueState.Busy;
                    OnTrigger?.Invoke(s, name, () => {
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
}