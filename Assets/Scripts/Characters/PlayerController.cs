using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float interactRadius = 0.2f;

    // Declare components for caching
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] public CharacterController characterController = null;
    [SerializeField] public CombatController combatController = null;

    [SerializeField] private ActionSlot _actionSlot;

    private Collider2D _collider = null;

    public Vector2 characterRotation {get; private set;}

    private InteractableObject closestObject = null;

    public static PlayerController instance {get; private set;} = null;

    private bool _attacking = false;

    public void GiveSiphon() {
        _actionSlot.AddAbilityByIndex(0);
    }

    public void TakeSiphon() {
        _actionSlot.RemoveAbilityByIndex(0);
    }

    public void SetCamera(Camera newCamera) {
        _mainCamera = newCamera;
    }

    void Awake() {
        instance = this;

        // Override components if they haven't been set in the inspector
        if (_mainCamera == null) _mainCamera = Camera.main;
        if (characterController == null) characterController = GetComponent<CharacterController>();
        if (combatController == null) combatController = GetComponent<CombatController>();
        if (_collider == null) _collider = GetComponent<Collider2D>();
    }

    private void Start() {
        StartCoroutine(checkInteractable());
    }

    void OnEnable() {
        InputManager.OnInteract += Interact;
        InputManager.OnStopInteract += StopInteract;
        InputManager.OnMouseClickLeft += Attack;
        InputManager.OnMouseUpLeft += StopAttack;
    }

    void OnDisable() {
        InputManager.OnInteract -= Interact;
        InputManager.OnStopInteract -= StopInteract;
        InputManager.OnMouseClickLeft -= Attack;
        InputManager.OnMouseUpLeft -= StopAttack;
    }

    private void OnDestroy() {
        InputManager.OnInteract -= Interact;
        InputManager.OnStopInteract -= StopInteract;
        InputManager.OnMouseClickLeft -= Attack;
        InputManager.OnMouseUpLeft -= StopAttack;
    }

    public void Attack() {
        _attacking = true;
        StartCoroutine(attacking());
    }

    public void StopAttack() {
        _attacking = false;
    }

    IEnumerator attacking() {
        while(_attacking && (!DialogueManager.InConversation && !TimelineController.InCutscene)) {
            combatController.Attack(characterRotation);
            yield return null; // TODO: Apply correct delay
        }
    }

    void Update() {
        
        // Update character movement. I really don't like the non-raw input, it feels too sluggish
        Vector2 inputAxis = new Vector2(InputManager.Horizontal, InputManager.Vertical);
        characterController.Move(inputAxis);

        // Update character rotation (angle of mouse relative to player)
        if(_mainCamera != null) {
            characterRotation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            characterController.SetSpriteRotation(characterRotation);
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
                    if (distance < lastDistance && collider.GetComponent<InteractableObject>() != null) {
                        lastDistance = distance;
                        closest = collider;
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
                    closestObject = closest.gameObject.GetComponent<InteractableObject>();
                    UIManager.instance.showTooltip(closestObject);
                }
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    void Interact() {
        closestObject?.Interact();
    }

    void StopInteract() {
        closestObject?.StopInteract();
    }
}
