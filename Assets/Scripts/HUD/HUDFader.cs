using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;
using UnityEngine; 
using UnityEngine.UI;

public class HUDFader : MonoBehaviour
{
    public float fadeTime;
    private float currentFadeTime;
    public Image hudImage;
    public Sprite[] SpriteArray;
    private Color C;
    public bool FadeIn;
    public Powerbar powerbar;

    void Start()
    {
        fadeTime = 3.0f;
        currentFadeTime = fadeTime;
        C = hudImage.color;
        FadeIn = true;
    }

    public void SwapSprite(int currentPower) {
        if (currentPower < 20) {
            hudImage.sprite = SpriteArray[0];
        } else if (currentPower >= 20 && currentPower < 40) {
            hudImage.sprite = SpriteArray[1];
        } else if (currentPower >= 40 && currentPower < 60) {
            hudImage.sprite = SpriteArray[2];
        } else if (currentPower >= 60 && currentPower < 80) {
            hudImage.sprite = SpriteArray[3];
        } else if (currentPower >= 80) {
            hudImage.sprite = SpriteArray[4];
        }
    }

    void Update()
    //Controls the imagealpha for the fading UI animation
    {
        if (FadeIn == true)
        {
            if (currentFadeTime <= 0)
            {
                FadeIn = false;
                currentFadeTime = 0;
            }
            else
            {
                currentFadeTime -= Time.deltaTime;
                C.a = Mathf.Clamp01(currentFadeTime / fadeTime);
                hudImage.color = C;
            }
        }
        else
        {
            if (currentFadeTime >= fadeTime)
            {
                FadeIn = true;
                currentFadeTime = fadeTime;
            }
            else
            {
                currentFadeTime += Time.deltaTime;
                C.a = Mathf.Clamp01(currentFadeTime / fadeTime);
                hudImage.color = C;
            }
        }
    }
}
