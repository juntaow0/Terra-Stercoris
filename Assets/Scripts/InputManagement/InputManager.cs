using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this class might not be necessary
public class InputManager : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    public static InputManager instance;
    public static event Action OnPause;
    public static event Action OnMouseClickLeft;
    public static event Action OnMouseClickRight;
    public static event Action OnEscape;
    public static event Action OnEnter;
    // more to be added based on need
    public KeyCode LEFT;
    public KeyCode RIGHT;
    public KeyCode UP;
    public KeyCode DOWN;
    public KeyCode FIRE;

    private void Awake() {
        if (instance!=null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        // more to be added based on need
    }
}
