using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// intended for loading scenes with unity event
public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByIndex(int index) {
        SceneManager.LoadScene(index);
    }

    public void LoadSceneByName(string name) {
        SceneManager.LoadScene(name);
    }
}
