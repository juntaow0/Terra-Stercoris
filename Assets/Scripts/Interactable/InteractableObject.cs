using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour {

    [SerializeField] public string message;
    [SerializeField] private UnityEvent interactAction;
    [SerializeField] private UnityEvent stopInteractAction;
    [SerializeField] private bool SelfDestruct = false;
    [SerializeField] private bool _isEnabled = true;

    public bool IsEnabled {get {return _isEnabled;} set {_isEnabled = value;}}

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
