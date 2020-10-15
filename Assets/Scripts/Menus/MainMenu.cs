using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas credit;
    [SerializeField] private Canvas buttons;
    private TransitionController tc;

    private void Awake() {
        tc = GetComponent<TransitionController>();
    }

    private void Start() {
        credit.enabled = false;
    }

    public void ShowCredit() {
        buttons.enabled = false;
        credit.enabled = true;
    }

    public void Back() {
        credit.enabled = false;
        buttons.enabled = true;
    }

    public void StartGame() {
        tc.LoadSceneByName("Intro");
    }
}
