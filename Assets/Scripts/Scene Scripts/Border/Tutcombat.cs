using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tutcombat : MonoBehaviour
{
    [SerializeField] private int healthThreshold = 15;
    [SerializeField] private UnityEvent surrender;
    [SerializeField] private CharacterController tutCharController;
    // Start is called before the first frame update
    void Start()
    {
        tutCharController.health.OnResourceUpdated += CheckForLowHealth;
    }

    void CheckForLowHealth(int health) {
        if (health <= healthThreshold) {
            surrender?.Invoke();
            tutCharController.health.OnResourceUpdated -= CheckForLowHealth;
            enabled = false;
        }
    }
}
