using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SemiAutoDoor : MonoBehaviour, IInteractable {

    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;
    private Collider2D collider;
    private SpriteRenderer renderer;
    private bool isOpen;
    private string _message = "Open Door";
    public string message {get {return _message;} set{_message = value;}}
    public bool InteractEnabled {get {return !isOpen;} set {}}

    void Start() {
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
        Close();
    }

    public void Interact() {
        Open();
    }

    public void StopInteract() {}

    public void Close() {
        isOpen = false;
        renderer.sprite = closedSprite;
        collider.isTrigger = false;
    }

    public void Open() {
        isOpen = true;
        renderer.sprite = openSprite;
        collider.isTrigger = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        Close();
    }
}
