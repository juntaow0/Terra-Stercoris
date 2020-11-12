using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tuskcombat : MonoBehaviour
{
    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private CharacterController TuskCC;
    // Start is called before the first frame update
    void Start() {
        TuskCC = GetComponent<CharacterController>();
        TuskCC.health.OnResourceUpdated += DeathEvent;
    }

    void DeathEvent(int health) {
        if (health <= 0) {
            OnDeath?.Invoke();
            TuskCC.health.OnResourceUpdated -= DeathEvent;
            enabled = false;
        }
    }
}
