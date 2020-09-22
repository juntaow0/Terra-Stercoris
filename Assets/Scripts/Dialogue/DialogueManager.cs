using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private Queue<Sentence> sentences;
    private List<StandingPicture> speakers;
    private void Awake() {
        if (instance!=null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
        sentences = new Queue<Sentence>();
        speakers = new List<StandingPicture>();
    }

    public void LoadConversation(Conversation conversation) {
        sentences.Clear();
        speakers.Clear();
        foreach (Sentence s in conversation.sentences) {
            sentences.Enqueue(s);
        }
        foreach (StandingPicture sp in conversation.speakers) {
            speakers.Add(sp);
        }
        DisplaySentence();
    }

    public void DisplaySentence() {
        if (sentences.Count < 1) {
            Debug.Log("end of conversation");
            return;
        }
        Sentence s = sentences.Dequeue();
        StandingPicture sp = speakers[s.speakerIndex];
        Debug.Log(sp.characterName + ": "+ s.sentence);
    }
}
