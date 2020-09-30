using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this class might not be necessary
public class InputManager : MonoBehaviour
{
    public static float Horizontal { get; private set; }
    public static float Vertical { get; private set; }

    public static InputManager instance;
    public static event Action OnPause;
    public static event Action OnMouseClickLeft;
    public static event Action OnMouseClickRight;
    public static event Action OnEscape;
    public static event Action OnEnter;
    public static event Action OnInteract;
    public static event Action OnStopInteract; // Used for actions when holding down interact key
    public static event Action OnNextDialogue;

    public Tooltip tooltip;

    // more to be added based on need
    public KeyCode LEFT;
    public KeyCode RIGHT;
    public KeyCode UP;
    public KeyCode DOWN;
    public KeyCode FIRE;
    public KeyCode PAUSE;
    public KeyCode INTERACT;

    private void Awake() {
        if (instance!=null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    void Update()
    {
        // more to be added based on need

        if(Input.GetKeyDown(PAUSE)) {
            OnPause?.Invoke();
        }

        if(!DialogueManager.InConversation) {
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
            if(Input.GetKeyDown(INTERACT)) {
                OnInteract?.Invoke();
            }
            if(Input.GetKeyUp(INTERACT)) {
                OnStopInteract?.Invoke();
            }
            if(Input.GetMouseButtonDown(0)) {
                OnMouseClickLeft?.Invoke();
            }
        } else {
            Horizontal = 0;
            Vertical = 0;

            if(Input.GetKeyDown(INTERACT) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                OnNextDialogue?.Invoke();
            }
        }
    }
}
