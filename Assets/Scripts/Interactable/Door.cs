using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Door : MonoBehaviour, IInteractable {

    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    [SerializeField] private bool startOpen = false;
    [SerializeField] private string doorSound;
    private Collider2D collider;
    private SpriteRenderer renderer;
    private bool isOpen;
    private string _message;
    public string message {get {return _message;} set{_message = value;}}
    public bool InteractEnabled {get {return true;} set {}}

    void Start() {
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
        if(startOpen) {
            Open();
        } else {
            Close();
        }
    }

    public void Interact() {
        AudioManager.instance.Play(doorSound);
        if(isOpen) {
            Close();
        } else {
            Open();
        }
    }

    public void StopInteract() {}

    public void Close() {
        isOpen = false;
        renderer.sprite = closedSprite;
        collider.isTrigger = false;
        message = "Open Door";
    }

    public void Open() {
        isOpen = true;
        renderer.sprite = openSprite;
        collider.isTrigger = true;
        message = "Close Door";
    }
}
