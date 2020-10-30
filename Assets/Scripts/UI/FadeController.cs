using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public void FadeToBlack() {
        UIManager.instance.FadeIn(1f,null);
    }

    public void FadeFromBlack() {
        UIManager.instance.FadeOut(1f, null);
    }
}
