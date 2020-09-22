using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    private Queue<Sentence> sentences;
    private List<StandingPicture> speakers;
    private List<Choice> choices;

    private void Awake() {
        if (instance!=null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
        sentences = new Queue<Sentence>();
        speakers = new List<StandingPicture>();
        choices = new List<Choice>();
    }

    public void LoadConversation(Conversation conversation) {
        sentences.Clear();
        speakers.Clear();
        choices.Clear();
        foreach (Sentence s in conversation.sentences) {
            sentences.Enqueue(s);
        }
        foreach (StandingPicture sp in conversation.speakers) {
            speakers.Add(sp);
        }
        if (conversation.choices!=null) {
            foreach (Choice c in conversation.choices.choices) {
                choices.Add(c);
            }
        }
        DisplaySentence();
    }

    public void DisplaySentence() {
        if (sentences.Count < 1) {
            if (choices.Count == 0) {
                Debug.Log("end of conversation");
                return;
            }
            int index = Random.Range(0, choices.Count);
            LoadConversation(choices[index].conversation);
            return;
        }
        Sentence s = sentences.Dequeue();
        StandingPicture sp = speakers[s.speakerIndex];
        Debug.Log(sp.characterName + ": "+ s.sentence);
        
    }


}
