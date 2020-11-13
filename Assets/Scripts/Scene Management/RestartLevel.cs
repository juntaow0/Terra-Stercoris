using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    private CharacterController playerCC;

    private void Awake() {
        playerCC = GetComponent<CharacterController>();
        playerCC.OnDeath += Restart;
    }

    void Restart() {
        playerCC.OnDeath -= Restart;
        UIManager.instance.ShowDeathScreen();
    }
}
