using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// this class might not be necessary
public class InputManager : MonoBehaviour
{
    public static float Horizontal { get; private set; }
    public static float Vertical { get; private set; }

    public static InputManager instance;
    public static event Action OnPause;
    public static event Action OnMouseClickLeft;
    public static event Action OnMouseClickRight;
    public static event Action OnMouseUpLeft;
    public static event Action OnMouseUpRight;
    public static event Action OnEscape;
    public static event Action OnEnter;
    public static event Action OnInteract;
    public static event Action OnStopInteract; // Used for actions when holding down interact key
    public static event Action OnNextDialogue;
    public static event Action<int> OnScroll;

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

        if(!DialogueManager.InConversation && !TimelineController.InCutscene) {
            Horizontal = Input.GetAxisRaw("Horizontal");
            Vertical = Input.GetAxisRaw("Vertical");
            if(Input.GetKeyDown(INTERACT)) {
                OnInteract?.Invoke();
            }
            if(Input.GetKeyUp(INTERACT)) {
                OnStopInteract?.Invoke();
            }
            if(!EventSystem.current.IsPointerOverGameObject()) {
                if(Input.GetMouseButtonDown(0)) {
                    OnMouseClickLeft?.Invoke();
                }
                if(Input.GetMouseButtonDown(1)) {
                    OnMouseClickRight?.Invoke();
                }
                if(Input.GetMouseButtonUp(0)) {
                    OnMouseUpLeft?.Invoke();
                }
                if(Input.GetMouseButtonUp(1)) {
                    OnMouseUpRight?.Invoke();
                }
            }
        } else {
            Horizontal = 0;
            Vertical = 0;

            if(Input.GetKeyDown(INTERACT) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                OnNextDialogue?.Invoke();
            }
        }

        int scrollDirection = (int) Input.mouseScrollDelta.y;

        if(scrollDirection != 0) {
            OnScroll?.Invoke(scrollDirection);
        }
    }
}
