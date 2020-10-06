using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NonlinearStartup : MonoBehaviour {

    void Start() {

        Debug.LogWarning("This script is outdated. Please replace with SceneStartup.cs");

        if (InputManager.instance == null) {
            AsyncOperation scene = SceneManager.LoadSceneAsync("DefaultScene", LoadSceneMode.Additive);
        }

        Destroy(this.gameObject);
    }
}
