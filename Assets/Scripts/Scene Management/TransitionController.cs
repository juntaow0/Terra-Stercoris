using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour {

    public void LoadPlayerScene(string scene) {
        TransitionManager.instance.LoadScene(scene, true);
    }

    public void LoadCutsceneScene(string scene) {
        TransitionManager.instance.LoadScene(scene, false);
    }
}
