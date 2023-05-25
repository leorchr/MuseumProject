using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MaskStatus
{
    Full, Using, Empty, Charging
}

public class Mask : MonoBehaviour
{
    public static Mask instance;

    [SerializeField] private GameObject[] plateforms;
    private int length;

    private float timeActivation;
    public float duration;

    //private float speedRecharging; // ajout de la speed plus tard
    private float rechargingActivation;

    [SerializeField] private float cooldown;
    private float cooldownRemaining;
    private float cooldownActivation;
    private bool ableToUse;

    [HideInInspector] public float timeRemaining;
    private float currentTimeRemaining;
    private MaskStatus maskStatus = MaskStatus.Full;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        maskStatus = MaskStatus.Full;
        timeRemaining = duration;
        ableToUse = true;
        length = plateforms.Length;
        PlateformOff();
    }

    private void Update()
    {
        switch (maskStatus)
        {
            case MaskStatus.Using:
                timeRemaining = duration - (duration-currentTimeRemaining) - (Time.time - timeActivation);
                if(timeRemaining <= 0)
                {
                    PlateformOff();
                    maskStatus = MaskStatus.Empty;
                    cooldownActivation = Time.time;
                    ableToUse = false;
                }
                break;
            case MaskStatus.Empty:
                cooldownRemaining = cooldown - (Time.time - cooldownActivation);
                if(cooldownRemaining <= 0)
                {
                    rechargingActivation = Time.time;
                    maskStatus= MaskStatus.Charging;
                }
                break;
            case MaskStatus.Charging:
                timeRemaining = Time.time - rechargingActivation;
                if(timeRemaining >= duration)
                {
                    timeRemaining = duration;
                    maskStatus = MaskStatus.Full;
                    ableToUse = true;
                }
                break;
            case MaskStatus.Full:
                break;
        }
    }

    public void PlateformOn()
    {
        timeActivation = Time.time;
        currentTimeRemaining = timeRemaining;
        maskStatus = MaskStatus.Using;
        for (int i = 0; i < length; i++)
        {
            plateforms[i].SetActive(true);
        }
    }

    public void PlateformOff()
    {
        for (int i = 0; i < length; i++)
        {
            plateforms[i].SetActive(false);
        }
    }

    public void UseMask(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (ableToUse)
            {
                if (maskStatus == MaskStatus.Full || maskStatus == MaskStatus.Charging)
                {
                    PlateformOn();
                }
                else if (maskStatus == MaskStatus.Using)
                {
                    PlateformOff();
                    rechargingActivation = Time.time - timeRemaining;
                    maskStatus = MaskStatus.Charging;
                }
            }
        }
    }
}
