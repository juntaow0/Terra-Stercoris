using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    void ResetStats() {
        PlayerController.instance.characterController.SetEnergy(100);
        PlayerController.instance.characterController.SetHealth(100);
    }
    public void ReloadLevel() {
        string sceneName = SceneManager.GetActiveScene().name;
        ResetStats();
        SceneDataLoader.SaveStates();
        TransitionManager.instance.LoadScene(sceneName);
    }
}
