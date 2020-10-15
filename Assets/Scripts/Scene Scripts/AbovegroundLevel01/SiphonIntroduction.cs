using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SiphonIntroduction : MonoBehaviour {

    [SerializeField] private int healthThreshold = 15;
    [SerializeField] private int startHealth = 1000;
    [SerializeField] private CharacterController _monsterCharController;

    [SerializeField] private UnityEvent startSiphonCutscene;
    [SerializeField] private UnityEvent afterDeathCutscene;

    void Start() {
        _monsterCharController.health.max = startHealth;
        _monsterCharController.health.quantity = startHealth;
        _monsterCharController.OnDeath += afterDeathCutscene.Invoke;
        PlayerController.instance.characterController.health.OnResourceUpdated += CheckForLowHealth;
    }

    void CheckForLowHealth(int health) {
        if(health <= healthThreshold) {
            startSiphonCutscene?.Invoke();
            PlayerController.instance.GiveSiphon();
            PlayerController.instance.characterController.health.OnResourceUpdated -= CheckForLowHealth;
            this.enabled = false;
        }
    }

    public void RemoveSiphon() {
        PlayerController.instance.TakeSiphon();
    }
}
