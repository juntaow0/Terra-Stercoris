using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueUI))]
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject UIPrefab;
    public GameObject buttonPrefab;
    private DialogueUI dialogueUI;
    private Queue<Sentence> sentences;
    private Conversation currentConversation;
    public static event Action<string,string> OnTrigger;
    public static event Action<Choice[]> OnChoice;

    private void Awake() {
        if (instance!=null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
        dialogueUI = GetComponent<DialogueUI>();
        sentences = new Queue<Sentence>();
        dialogueUI.InitializeUI(UIPrefab, buttonPrefab);
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
        DisplaySentence();
    }

    private void RunEndAction() {
        switch (currentConversation.endAction) {
            case EndAction.NONE:
                dialogueUI.toggleDialogueBox(false);
                break;
            case EndAction.CHOICE:
                dialogueUI.toggleChoices(true);
                break;
            case EndAction.CONVERSATION:
                LoadConversation(currentConversation.nextConversation);
                break;
            case EndAction.EVENT:
                currentConversation.endEvent?.Invoke();
                break;
        }
    }

    public void DisplaySentence() {
        if (sentences.Count < 1) {
            RunEndAction();
            return;
        }
        Sentence s = sentences.Dequeue();
        string name = currentConversation.speakers[s.speakerIndex];
        OnTrigger?.Invoke(s.sentence,name);
    }
}