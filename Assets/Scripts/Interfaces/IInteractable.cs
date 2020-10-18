using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {

    string message {get; set;}
    bool InteractEnabled {get; set;}

    Transform transform {get;}

    void Interact();
    void StopInteract();
}
