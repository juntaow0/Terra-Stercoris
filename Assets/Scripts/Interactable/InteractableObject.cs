using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable {

    [SerializeField] private string _message;
    [SerializeField] private UnityEvent interactAction;
    [SerializeField] private UnityEvent stopInteractAction;
    [SerializeField] private bool SelfDestruct = false;
    [SerializeField] private bool _isEnabled = true;

    public string message {get {return _message;} set {_message = value;}}
    public bool InteractEnabled {get {return _isEnabled;} set {_isEnabled = value;}}
    public Transform transform {get {return gameObject.transform;}}

    public void Interact() {
        interactAction?.Invoke();
        if(SelfDestruct) {
            Destroy(gameObject);
        }
    }

    public void StopInteract() {
        stopInteractAction?.Invoke();
    }
}
