using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SiphonIntroduction : MonoBehaviour {

    [SerializeField] private UnityEvent startSiphonCutscene;
    [SerializeField] private SceneStartup startup;

    void Start() {
        startup.OnSceneLoad += LateStart;
    }

    void LateStart() {
        PlayerController.instance.characterController.health.OnResourceUpdated += CheckForLowHealth;
        startup.OnSceneLoad -= LateStart;
    }

    void CheckForLowHealth(int health) {
        if(health <= 10) {
            startSiphonCutscene?.Invoke();
        }
    }
}
