using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    // Declare constants
    private const int NUM_ROTATIONS = 8;

    // Declare components for caching
    [SerializeField] private CharacterMovementController _movementController = null;
    [SerializeField] private Camera _mainCamera = null;

    // Character attributes (TODO: Potentially split into a separate "CharacterController")
    // It all really depends on how many components we actually want everything split into
    [SerializeField] private float _movementSpeed = 15.0f;
    [SerializeField] private int _spriteRotation = 0;

    void Start() {
        // Override components if they haven't been set in the inspector
        if (_movementController == null) _movementController = GetComponent<CharacterMovementController>();
        if (_mainCamera == null) _mainCamera = Camera.main;
    }

    void Update() {
        
        // Update character movement. I really don't like the non-raw input, it feels too sluggish
        Vector2 inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _movementController.Move(inputAxis.normalized * _movementSpeed);

        // Update character rotation (angle of mouse relative to player)
        Vector2 rotation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _spriteRotation = (NUM_ROTATIONS + (int) Mathf.Round(Mathf.Atan2(rotation.y, rotation.x) /
                (2*Mathf.PI) * NUM_ROTATIONS)) % NUM_ROTATIONS;
    }
}
