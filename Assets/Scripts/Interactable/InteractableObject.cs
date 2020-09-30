using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour {

    [SerializeField] public string message;
    [SerializeField] private UnityEvent interactAction;
    [SerializeField] private UnityEvent stopInteractAction;

    public void Interact() {
        interactAction?.Invoke();
    }

    public void StopInteract() {
        stopInteractAction?.Invoke();
    }
}
