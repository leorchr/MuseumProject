using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeDeath : MonoBehaviour
{
    public static FadeDeath instance;

    [Header("References")]
    public GameObject imageFade;
    public CanvasGroup canvasGroup;

    private bool fadeIn, fadeOut;

    [Header("Variable")]
    public float speedToFade;
    public float timeBetweenFade;
    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }
    
    void Update()
    {
        if (fadeIn)
        {
            if (canvasGroup.alpha < 1)
            {
                Debug.Log(speedToFade * Time.deltaTime);
                canvasGroup.alpha += speedToFade * Time.deltaTime;
            }
            if (canvasGroup.alpha >= 1)
            { 
                fadeIn = false;
                Invoke("FadeOut", timeBetweenFade);
            }
        }
        if (fadeOut)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= speedToFade * Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    fadeOut = false;
                    
                }
            }
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
    }


    public void FadeOut()
    {
        fadeOut = true;
        DeathPlayer.instance.Respawn();
    }
}
