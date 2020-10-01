using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour {

    [SerializeField] public string message;
    [SerializeField] private UnityEvent interactAction;
    [SerializeField] private UnityEvent stopInteractAction;
    [SerializeField] private bool SelfDestruct = false;

    public void Interact() {
        interactAction?.Invoke();
        if(SelfDestruct) {
            Destroy(this);
        }
    }

    public void StopInteract() {
        stopInteractAction?.Invoke();
    }
}
