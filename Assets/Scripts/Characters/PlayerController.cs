using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    // Declare constants
    private const int NUM_ROTATIONS = 8;

    [SerializeField] private float interactRadius = 0.2f;

    // Declare components for caching
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private CharacterController _characterController = null;
    [SerializeField] private Healthbar _healthBar = null;
    [SerializeField] private Powerbar _powerBar = null;
    private Collider2D _collider = null;

    private InteractableObject closestObject = null;

    void Awake() {
        // Override components if they haven't been set in the inspector
        if (_mainCamera == null) _mainCamera = Camera.main;
        if (_characterController == null) _characterController = GetComponent<CharacterController>();
        if (_collider == null) _collider = GetComponent<Collider2D>();
    }

    void OnEnable() {
        InputManager.OnInteract += Interact;
        _characterController.health.OnResourceUpdated += UpdateHealth;
        _characterController.energy.OnResourceUpdated += UpdateEnergy;
    }

    void OnDisable() {
        InputManager.OnInteract -= Interact;
        _characterController.health.OnResourceUpdated -= UpdateHealth;
        _characterController.energy.OnResourceUpdated -= UpdateEnergy;
    }

    void UpdateHealth(int newHealth) {
        _healthBar?.setHealth(newHealth);
    }

    void UpdateEnergy(int newPower) {
        _powerBar?.setPower(newPower);
    }

    void Update() {
        
        // Update character movement. I really don't like the non-raw input, it feels too sluggish
        Vector2 inputAxis = new Vector2(InputManager.Horizontal, InputManager.Vertical);
        _characterController.Move(inputAxis.normalized * _characterController.GetSpeed());

        // Update character rotation (angle of mouse relative to player)
        Vector2 rotation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _characterController.SetSpriteRotation((NUM_ROTATIONS + (int) Mathf.Round(Mathf.Atan2(rotation.y, rotation.x) /
                (2*Mathf.PI) * NUM_ROTATIONS)) % NUM_ROTATIONS);

        Collider2D closest = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        float lastDistance = float.MaxValue;
        foreach (Collider2D collider in colliders) {
            if (collider != _collider) {
                float distance = collider.Distance(_collider).distance;
                if (distance < lastDistance) {
                    lastDistance = distance;
                    closest = collider;
                }
            }
        }
        if(closest != null) {
            closestObject = closest.gameObject.GetComponent<InteractableObject>();
            closestObject?.DisplayTooltip();
        } else {
            InputManager.instance.tooltip.gameObject.SetActive(false);
        }
    }

    void Interact() {
        if(closestObject != null) {
            closestObject.Interact();
        }
    }
}
