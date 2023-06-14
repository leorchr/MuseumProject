using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

//sur le joueur

public class PopUpMask : MonoBehaviour
{
    public static PopUpMask instance;

    [HideInInspector] public bool isPopUp;


    public void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    public void Start()
    {
        isPopUp = false;
    }
    public void ActivatePopUp()
    {
        //stopper le temps;
        //afficher l'image
        FadePopUp.instance.FadeIn();
        PlayerController.instance.sprintSpeed = 0;
        PlayerController.instance.walkSpeed = 0;
        PlayerController.instance.moveSpeed = PlayerController.instance.walkSpeed;
        isPopUp = true;

    }

    public void Confirmed(InputAction.CallbackContext context)
    {
        //appuyer sur un bouton pour passer (autre fonction)
        FadePopUp.instance.FadeOut();
        PlayerController.instance.sprintSpeed = 8f;
        PlayerController.instance.walkSpeed = 4.5f;
        PlayerController.instance.moveSpeed = PlayerController.instance.walkSpeed;
        isPopUp = false;


    }

    public void StopTime()
    {
        //Time.timeScale = 0;
    }


}
