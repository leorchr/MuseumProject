using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

//sur le joueur

public class PopUpMask : MonoBehaviour
{
    public static PopUpMask instance;
    public PlayerController playerContr;


    public void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    public void Start()
    {
    }
    public void ActivatePopUp()
    {
        //stopper le temps;
        //afficher l'image
        FadePopUp.instance.FadeIn();
        playerContr.enabled = false;
    }

    public void Confirmed(InputAction.CallbackContext context)
    {
        //appuyer sur un bouton pour passer (autre fonction)
        FadePopUp.instance.FadeOut();
        //remettre le temps a 1;
        playerContr.enabled=true;

    }

    public void StopTime()
    {
        //Time.timeScale = 0;
    }


}
