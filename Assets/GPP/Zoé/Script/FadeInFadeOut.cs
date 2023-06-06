using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInFadeOut : MonoBehaviour
{
    public static FadeInFadeOut instance;

    public GameObject imageFade;

    public CanvasGroup canvasGroup;
    public bool fadeIn = false;
    public bool fadeOutChangingScene = false;
    public bool fadeOutDeath;

    public float speedToFade;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        imageFade.SetActive(true);
        canvasGroup.alpha = 0;
    }
    void Update()
    {
        if (fadeIn == true)
        {
            if (canvasGroup.alpha < 1)
            {
                if (DeathPlayer.instance.isDead)
                {
                    canvasGroup.alpha += DeathPlayer.instance.speedFade * Time.time;
                }
                else if (ChangingScene.instance.isChanging)
                {
                    canvasGroup.alpha += ChangingScene.instance.speedFade * Time.time;
                }
                if (canvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
        if (fadeOutChangingScene == true)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= speedToFade * Time.time;
                if (canvasGroup.alpha == 0)
                {
                    fadeOutChangingScene = false;
                }
            }
        }
        if(fadeOutDeath == true)
        {
            if (canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= speedToFade * Time.time;
                if (canvasGroup.alpha == 0)
                {
                    fadeOutDeath = false;
                }
            }
        }
    }

    public void FadeIn(float timeBetweenFade)
    {
        if (DeathPlayer.instance.isDead)
        {
            fadeIn = true;
            Invoke("FadeOutDeath", timeBetweenFade);
        }
        else if (ChangingScene.instance.isChanging)
        {
            fadeIn = true;
            Invoke("FadeOutChangingScene", timeBetweenFade);
        }
    }
    public void FadeOutChangingScene()
    {
        fadeOutChangingScene = true;
    }

    public void FadeOutDeath()
    {
        fadeOutDeath = true;
        DeathPlayer.instance.Respawn();
    }

}
