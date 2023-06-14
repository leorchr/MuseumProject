using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FadePopUp : MonoBehaviour
{
    public static FadePopUp instance;

    public bool fadeIn;
    public bool fadeOut;

    public CanvasGroup canvasGroup;

    public int timeBetweenFade;
    public int speedToFade;

    public void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            if (canvasGroup.alpha < 1)
            {
                //Debug.Log(speedToFade * Time.deltaTime);
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
    }

    
    
}
