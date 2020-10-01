using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NonlinearStartup : MonoBehaviour {

    void Start() {
        if (InputManager.instance == null) {
            AsyncOperation scene = SceneManager.LoadSceneAsync("DefaultScene", LoadSceneMode.Additive);
        }

        Destroy(this.gameObject);
    }
}
