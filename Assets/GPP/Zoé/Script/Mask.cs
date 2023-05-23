using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mask : MonoBehaviour
{
    public static Mask instance;

    [Space]
    [Header("Plateform\n--------------")]
    public bool isActivate;
    public GameObject platerforms;


    public float timeActivation;
    public float duration;

    private void Awake()
    {
        if (instance) Destroy(this);
        instance = this;
    }

    void Start()
    {
        isActivate = false;
        platerforms.SetActive(false);
    }

    void Update()
    {
        if (isActivate)
        {
            if(timeActivation + duration < Time.time)
            {
                PlateformOff();
            }
        }
    }

    public void PlateformOn()
    {

        isActivate = true;
        platerforms.SetActive(true);
    }

    public void PlateformOff()
    {
        isActivate = false;
        platerforms.SetActive(false); 
    }

    public void UseMask(InputAction.CallbackContext context)
    {
        timeActivation = Time.time;
        isActivate = true;
        platerforms.SetActive(true);
    }
}

