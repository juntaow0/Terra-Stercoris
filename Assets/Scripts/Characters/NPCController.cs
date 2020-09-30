using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    [SerializeField] private CharacterController _characterController = null;

    void Awake() {
        if (_characterController == null) _characterController = GetComponent<CharacterController>();
    }

    void OnEnable() {
        _characterController.health.OnResourceUpdated += HealthUpdate;
    }

    void OnDisable() {
        _characterController.health.OnResourceUpdated -= HealthUpdate;
    }

    void OnDestroy() {
        _characterController.health.OnResourceUpdated -= HealthUpdate;
    }

    void HealthUpdate(int health) {
        Debug.Log("Health: " + health);
        if(health <= 0) {
            // Placeholder
            Destroy(gameObject);
        }
    }
}
