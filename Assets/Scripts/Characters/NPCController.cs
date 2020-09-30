using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    [SerializeField] private CharacterController _characterController = null;

    void Awake() {
        if (_characterController == null) _characterController = GetComponent<CharacterController>();
    }

    void OnEnable() {
        _characterController.health.OnResourceUpdated += DisplayHealth;
    }

    void OnDisable() {
        _characterController.health.OnResourceUpdated -= DisplayHealth;
    }

    void OnDestroy() {
        _characterController.health.OnResourceUpdated -= DisplayHealth;
    }

    void DisplayHealth(int amount) {
        Debug.Log("Health: " + amount);
    }
}
