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
    [SerializeField] private CombatController _combatController = null;
    [SerializeField] private Healthbar _healthBar = null;
    [SerializeField] private Powerbar _powerBar = null;
    private Collider2D _collider = null;

    private Vector2 _characterRotation;

    private InteractableObject closestObject = null;

    void Awake() {
        // Override components if they haven't been set in the inspector
        if (_mainCamera == null) _mainCamera = Camera.main;
        if (_characterController == null) _characterController = GetComponent<CharacterController>();
        if (_combatController == null) _combatController = GetComponent<CombatController>();
        if (_collider == null) _collider = GetComponent<Collider2D>();
    }

    private void Start() {
        _healthBar.bindHealthBar(_characterController.health.max,_characterController.GetHealth());
        _powerBar.bindPowerBar(_characterController.energy.max, _characterController.GetEnergy());

        StartCoroutine(checkInteractable());
    }

    void OnEnable() {
        InputManager.OnInteract += Interact;
        InputManager.OnStopInteract += StopInteract;
        InputManager.OnMouseClickLeft += Attack;
        _characterController.health.OnResourceUpdated += UpdateHealth;
        _characterController.energy.OnResourceUpdated += UpdateEnergy;
    }

    void OnDisable() {
        InputManager.OnInteract -= Interact;
        InputManager.OnStopInteract -= StopInteract;
        InputManager.OnMouseClickLeft -= Attack;
        _characterController.health.OnResourceUpdated -= UpdateHealth;
        _characterController.energy.OnResourceUpdated -= UpdateEnergy;
    }

    private void OnDestroy() {
        InputManager.OnInteract -= Interact;
        InputManager.OnStopInteract -= StopInteract;
        InputManager.OnMouseClickLeft -= Attack;
        _characterController.health.OnResourceUpdated -= UpdateHealth;
        _characterController.energy.OnResourceUpdated -= UpdateEnergy;
    }

    void Attack() {
        _combatController.Attack(_characterRotation);
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
        _characterController.Move(inputAxis.normalized * _characterController.GetSpeed());

        // Update character rotation (angle of mouse relative to player)
        _characterRotation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _characterController.SetSpriteRotation((NUM_ROTATIONS + (int) Mathf.Round(Mathf.Atan2(_characterRotation.y, _characterRotation.x) /
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
                    if (distance < lastDistance) {
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
                if(closest.gameObject != closestObject?.gameObject) {
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
