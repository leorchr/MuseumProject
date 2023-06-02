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

    public Material materialPlateformOn;
    public Material materialPlateformOff;
    //public MeshRenderer meshRenderer;
    public float currentValueOn;
    public float currentValueOff;
    public float increaseSpeed;

    public bool platOnBool;
    public bool platOffBool;


    [SerializeField] private GameObject[] plateformsOn;
    [SerializeField] private GameObject[] plateformsOff;
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

    public bool opacity = false;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        //meshRenderer = GetComponent<MeshRenderer>();
        maskStatus = MaskStatus.Full;
        timeRemaining = duration;
        ableToUse = true;
        PlateformOff();
        opacity = false;
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
        for (int i = 0; i < plateformsOn.Length; i++)
        {
            //SetOpacity(currentValueOn, materialPlateformOn);
            plateformsOn[i].SetActive(true);
        }
        for (int i = 0; i < plateformsOff.Length; i++)
        {
            plateformsOff[i].SetActive(false);
            //SetOpacity(currentValueOff, materialPlateformOff);
        }
    }

    public void PlateformOff()
    {
        for (int i = 0; i < plateformsOn.Length; i++)
        {
            plateformsOn[i].SetActive(false);
            //SetOpacity(currentValueOn, materialPlateformOn);
        }
        for (int i = 0; i < plateformsOff.Length; i++)
        {
            plateformsOff[i].SetActive(true);
            //SetOpacity(currentValueOff, materialPlateformOff);
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

    /*public void SetOpacity(float opacity, Material mat)
    {
 
        if (currentValueOff == 0)
        {
            currentValueOff = Mathf.Lerp(currentValueOff, 255, increaseSpeed * Time.time);
        }
        else if (currentValueOff == 1)
        {
            currentValueOff = Mathf.Lerp(currentValueOff, 0, increaseSpeed * Time.time);
        }

        if (currentValueOn == 0)
        {
            currentValueOn = Mathf.Lerp(currentValueOn, 255, increaseSpeed * Time.time);
        }
        else if (currentValueOn == 1)
        {
            currentValueOn = Mathf.Lerp(currentValueOn, 0, increaseSpeed * Time.time);
        }

        Color color = mat.color;
        color.a = opacity;
        mat.color = color;
    }*/
}
