using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour {

    public void LoadSceneByName(string scene) {
        AudioManager.instance.StopSong();
        SceneDataLoader.SaveStates();
        TransitionManager.instance.LoadScene(scene);
    }

    /*
    public void LoadCutsceneScene(string scene) {
        TransitionManager.instance.LoadScene(scene, false);
    }
    */
}
