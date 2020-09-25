using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour {

    [SerializeField] private string message;
    [SerializeField] private UnityEvent interactAction;

    public void Interact() {
        interactAction?.Invoke();
    }

    public void DisplayTooltip() {
        InputManager.instance.tooltip.gameObject.SetActive(true);
        InputManager.instance.tooltip.SetText(message);
        InputManager.instance.tooltip.transform.position = transform.position;
    }
}
