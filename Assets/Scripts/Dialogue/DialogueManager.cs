using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueUI))]
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public static event Action<string, string, Action> OnTrigger;
    public static event Action<Choice[]> OnChoice;
    public static event Action OnSkip;
    public GameObject UIPrefab;
    public GameObject buttonPrefab;
    private DialogueUI dialogueUI;
    private Queue<Sentence> sentences;
    private Conversation currentConversation;
    private DialogueState state;
    public static bool InConversation {get; private set;} = false;

    private void Awake() {
        if (instance!=null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
        dialogueUI = GetComponent<DialogueUI>();
        sentences = new Queue<Sentence>();
        dialogueUI.InitializeUI(UIPrefab, buttonPrefab);
        state = DialogueState.Idle;
    }

    public void LoadConversation(Conversation conversation) {
        sentences.Clear();
        currentConversation = conversation;
        foreach (Sentence s in conversation.sentences) {
            sentences.Enqueue(s);
        }
        if (conversation.endAction==EndAction.CHOICE) {
            OnChoice?.Invoke(conversation.choices.choices);
        }
        dialogueUI.toggleDialogueBox(true);
        state = DialogueState.Idle;
        InConversation = true;
        DisplaySentence();
    }

    private void RunEndAction() {
        switch (currentConversation.endAction) {
            case EndAction.NONE:
                dialogueUI.toggleDialogueBox(false);
                InConversation = false;
                break;
            case EndAction.CHOICE:
                dialogueUI.toggleChoices(true);
                state = DialogueState.WaitforChoice;
                break;
            case EndAction.CONVERSATION:
                LoadConversation(currentConversation.nextConversation);
                break;
            case EndAction.EVENT:
                string message = currentConversation.sendMessage.message;
                gameObject.SendMessage(message);
                dialogueUI.toggleDialogueBox(false);
                InConversation = false;
                break;
        }
    }

    public void DisplaySentence() {
        switch (state) {        
            case DialogueState.Idle:
                Sentence s = sentences.Dequeue();
                string name = currentConversation.speakers[s.speakerIndex];
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