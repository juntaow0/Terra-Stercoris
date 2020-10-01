using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    // Declare constants
    private const int NUM_ROTATIONS = 8;

    [SerializeField] private float interactRadius = 0.2f;

    // Declare components for caching
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] public CharacterController characterController = null;
    [SerializeField] public CombatController combatController = null;
    [SerializeField] private Healthbar _healthBar = null;
    [SerializeField] private Powerbar _powerBar = null;
    private Collider2D _collider = null;

    private Vector2 _characterRotation;

    private InteractableObject closestObject = null;

    public static PlayerController instance {get; private set;}

    void Awake() {
        instance = this;

        // Override components if they haven't been set in the inspector
        if (_mainCamera == null) _mainCamera = Camera.main;
        if (characterController == null) characterController = GetComponent<CharacterController>();
        if (combatController == null) combatController = GetComponent<CombatController>();
        if (_collider == null) _collider = GetComponent<Collider2D>();
    }

    private void Start() {
        _healthBar.bindHealthBar(characterController.health.max,characterController.GetHealth());
        _powerBar.bindPowerBar(characterController.energy.max, characterController.GetEnergy());

        StartCoroutine(checkInteractable());
    }

    void OnEnable() {
        InputManager.OnInteract += Interact;
        InputManager.OnStopInteract += StopInteract;
        InputManager.OnMouseDownLeft += Attack;
        characterController.health.OnResourceUpdated += UpdateHealth;
        characterController.energy.OnResourceUpdated += UpdateEnergy;
    }

    void OnDisable() {
        InputManager.OnInteract -= Interact;
        InputManager.OnStopInteract -= StopInteract;
        InputManager.OnMouseDownLeft -= Attack;
        characterController.health.OnResourceUpdated -= UpdateHealth;
        characterController.energy.OnResourceUpdated -= UpdateEnergy;
    }

    private void OnDestroy() {
        InputManager.OnInteract -= Interact;
        InputManager.OnStopInteract -= StopInteract;
        InputManager.OnMouseDownLeft -= Attack;
        characterController.health.OnResourceUpdated -= UpdateHealth;
        characterController.energy.OnResourceUpdated -= UpdateEnergy;
    }

    void Attack() {
        combatController.Attack(_characterRotation);
    }

    void UpdateHealth(int newHealth) {
        _healthBar?.updateHealthBarUI(newHealth);
    }

    void UpdateEnergy(int newPower) {
        _powerBar?.updatePowerBarUI(newPower);
    }

    void Update() {
        
        // Update character movement. I really don't like the non-raw input, it feels too sluggish
        Vector2 inputAxis = new Vector2(InputManager.Horizontal, InputManager.Vertical);
        characterController.Move(inputAxis.normalized * characterController.GetSpeed());

        // Update character rotation (angle of mouse relative to player)
        _characterRotation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        characterController.SetSpriteRotation((NUM_ROTATIONS + (int) Mathf.Round(Mathf.Atan2(_characterRotation.y, _characterRotation.x) /
                (2*Mathf.PI) * NUM_ROTATIONS)) % NUM_ROTATIONS);        
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
                    InputManager.instance.tooltip.Hide();
                }
            } else {
                if(closest != closestObject) {
                    closestObject = closest.gameObject.GetComponent<InteractableObject>();
                    InputManager.instance.tooltip.Show(closestObject);
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
