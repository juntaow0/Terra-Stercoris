using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{
    public RawImage FirstImage;
    public RawImage SecondImage;
    public DialogueController dc1;
    public DialogueController dc2;

    private void Start() {
        dc1.enabled = false;
        dc1.StartConversation();
    }

    public void ChangeImage() {
        FirstImage.enabled = false;
        SecondImage.enabled = true;
        Invoke("StartAnotherConversation", 1f);
    }

    private void StartAnotherConversation() {
        dc1.enabled = false;
        dc2.enabled = true;
        dc2.StartConversation();
    }

    public void LoadTutorial() {
        SceneManager.LoadScene(2);
    }
}
