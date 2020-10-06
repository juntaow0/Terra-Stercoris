using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStartup : MonoBehaviour {

    [SerializeField] private Collider2D _cameraBounds;

    void Start() {
        if (InputManager.instance == null) {
            AsyncOperation scene = SceneManager.LoadSceneAsync("DefaultScene", LoadSceneMode.Additive);
            scene.completed += LateStart;
        } else {
            LateStart();
        }
    }

    void LateStart(AsyncOperation temp = null) {
        Cinemachine.CinemachineConfiner confiner = 
                TransitionManager.instance.cameraController.GetComponent<Cinemachine.CinemachineConfiner>();
        confiner.m_BoundingShape2D = _cameraBounds;
    }
}
