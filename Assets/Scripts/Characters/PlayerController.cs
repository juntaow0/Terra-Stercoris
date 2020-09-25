using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    // Declare constants
    private const int NUM_ROTATIONS = 8;

    // Declare components for caching
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private CharacterController _characterController = null;

    void Start() {
        // Override components if they haven't been set in the inspector
        if (_mainCamera == null) _mainCamera = Camera.main;
        if (_characterController == null) _characterController = GetComponent<CharacterController>();
    }

    void Update() {
        
        // Update character movement. I really don't like the non-raw input, it feels too sluggish
        Vector2 inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _characterController.Move(inputAxis.normalized * _characterController.GetSpeed());

        // Update character rotation (angle of mouse relative to player)
        Vector2 rotation = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _characterController.SetSpriteRotation((NUM_ROTATIONS + (int) Mathf.Round(Mathf.Atan2(rotation.y, rotation.x) /
                (2*Mathf.PI) * NUM_ROTATIONS)) % NUM_ROTATIONS);
    }
}
