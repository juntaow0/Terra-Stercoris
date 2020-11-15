using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float interactRadius = 0.2f;

    // Declare components for caching
    private Camera _mainCamera = null;
    public CharacterController characterController {get; private set;}
    public WeaponController weaponController {get; private set;}

    private Animator animator;

    private Collider2D _collider = null;

    private IInteractable closestObject = null;

    public static PlayerController instance {get; private set;} = null;

    public void SetCamera(Camera newCamera) {
        _mainCamera = newCamera;
    }

    void Awake() {
        instance = this;

        // Override components if they haven't been set in the inspector
        _mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        _collider = GetComponent<Collider2D>();
        weaponController = GetComponent<WeaponController>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        StartCoroutine(checkInteractable());
    }

    void OnEnable() {
        InputManager.OnInteract += Interact;
        InputManager.OnStopInteract += StopInteract;
        InputManager.OnMouseClickLeft += weaponController.Attack;
    }

    void OnDisable() {
        InputManager.OnInteract -= Interact;
        InputManager.OnStopInteract -= StopInteract;
        InputManager.OnMouseClickLeft -= weaponController.Attack;
    }

    void OnDestroy() {
        InputManager.OnInteract -= Interact;
        InputManager.OnStopInteract -= StopInteract;
        InputManager.OnMouseClickLeft -= weaponController.Attack;
    }

    void Update() {
        
        // Update character movement. I really don't like the non-raw input, it feels too sluggish
        Vector2 inputAxis = new Vector2(InputManager.Horizontal, InputManager.Vertical);
        characterController.Move(inputAxis);

        // Update character rotation (angle of mouse relative to player)
        if (_mainCamera != null) {
            if (!(DialogueManager.InConversation || TimelineController.InCutscene)) {
                characterController.rotation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            } else {
                if (DialogueManager.InConversation) {
                    if (closestObject != null) {
                        characterController.rotation = closestObject.transform.position - transform.position;
                    } else {
                        characterController.rotation = new Vector2(0, -1);
                    }
                }else if (TimelineController.InCutscene) {
                    characterController.rotation = new Vector2(0, -1);
                }
            }
        }
    }

    IEnumerator checkInteractable() {

        while(true) {
            Collider2D closest = null;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
            float lastDistance = float.MaxValue;
            foreach (Collider2D collider in colliders) {
                if (collider != _collider) {
                    float distance = collider.Distance(_collider).distance;
                    if (distance < lastDistance) {
                        IInteractable interactable = collider.GetComponent<IInteractable>();
                        if(interactable != null && interactable.InteractEnabled) {
                            lastDistance = distance;
                            closest = collider;
                        }
                    }
                }
            }
            if(closest == null) {
                if(closestObject != null) {
                    closestObject = null;
                    UIManager.instance.hdieTooltip();
                }
            } else {
                if(closest != closestObject) {
                    closestObject = closest.gameObject.GetComponent<IInteractable>();
                    UIManager.instance.showTooltip(closestObject);
                }
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    void Interact() {
        if (closestObject != null) {
            closestObject?.Interact();
        }
    }

    void StopInteract() {
        closestObject?.StopInteract();
    }
}
