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
    public Sprite currentSprite;
    private Color C;
    public bool FadeIn;
    public Powerbar powerbar;

    private const int MAX_POWER = 100; // Link this to max power

    void Start()
    {
        fadeTime = 3.0f;
        currentFadeTime = fadeTime;
        C = hudImage.color;
        FadeIn = true;
        SwapSprite(100); // Link this instead to the starting power
    }

    public void SwapSprite(int currentPower) {
        currentSprite = SpriteArray[(currentPower-1) / (MAX_POWER / SpriteArray.Length)];
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
                // Only update sprite on begin of fade in
                hudImage.sprite = currentSprite;
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
