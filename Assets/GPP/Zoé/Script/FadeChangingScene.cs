using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeChangingScene : MonoBehaviour
{
    public static FadeChangingScene instance;

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
    // Start is called before the first frame update
    void Start()
    {
        imageFade.SetActive(true);
        canvasGroup.alpha = 1;
        fadeOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += speedToFade * Time.deltaTime;
            }
            if (canvasGroup.alpha >= 1)
            {
                fadeIn = false;
                Invoke("ChangeScene", timeBetweenFade);
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

    public void ChangeScene()
    {
        ChangingScene.instance.GoTo();
    }


}
