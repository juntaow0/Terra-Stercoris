using UnityEngine;
using System.Collections;

public class Redirection : MonoBehaviour
{
    private TransitionController tc;
    private void Awake() {
        tc = GetComponent<TransitionController>();
    }
    public void TrailerButton() {
        float value = Random.value;
        if (value > 0.5f) {
            tc.LoadSceneByName("IntroOld");
        } else {
            Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        }
    }
}
