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

    void Update()
    //Controls the imagealpha for the fading UI animation
    {
        if (powerbar.currentPower < 20)
        {
            hudImage.sprite = SpriteArray[0];
        }
        else if (powerbar.currentPower >= 20 && powerbar.currentPower < 40)
        {
            hudImage.sprite = SpriteArray[1];
        }
        else if (powerbar.currentPower >= 40 && powerbar.currentPower < 60)
        {
            hudImage.sprite = SpriteArray[2];
        }
        else if (powerbar.currentPower >= 60 && powerbar.currentPower < 80)
        {
            hudImage.sprite = SpriteArray[3];
        }
        else if (powerbar.currentPower >= 80)
        {
            hudImage.sprite = SpriteArray[4];
        }
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
