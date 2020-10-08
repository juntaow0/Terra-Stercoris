using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SiphonIntroduction : MonoBehaviour {

    [SerializeField] private int healthThreshold = 15;
    [SerializeField] private int startHealth = 1000;
    private CharacterController _characterController;

    [SerializeField] private UnityEvent startSiphonCutscene;
    [SerializeField] private UnityEvent afterDeathCutscene;
    [SerializeField] private SceneStartup startup;

    void Awake() {
        _characterController = GetComponent<CharacterController>();
        _characterController.health.quantity = startHealth;
        _characterController.health.max = startHealth;
        _characterController.OnDeath += afterDeathCutscene.Invoke;
        startup.OnSceneLoad += LateStart;
    }

    void LateStart() {
        PlayerController.instance.characterController.health.OnResourceUpdated += CheckForLowHealth;
        startup.OnSceneLoad -= LateStart;
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
