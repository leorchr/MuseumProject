using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum MaskStatus
{
    Full, Using, Empty
}

public class Mask : MonoBehaviour
{
    public static Mask instance;
    private bool isInsidePlat;

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
    [HideInInspector] public MaskStatus maskStatus = MaskStatus.Full;

    public float speedReduction;

    public float energies;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip[] clip;


    PlatformVisibility[] allPlatforms;
    bool maskPlatforms = true;
    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        allPlatforms = FindObjectsOfType<PlatformVisibility>();
        foreach(PlatformVisibility platform in allPlatforms)
        {
            platform.ShowMaskPlatforms(maskPlatforms);
        }
        maskStatus = MaskStatus.Full;
        timeRemaining = duration;
        ableToUse = true;

    }
    

    private void Update()
    {
        switch (maskStatus)
        {
            case MaskStatus.Using:
                timeRemaining = duration - (duration - currentTimeRemaining) - (Time.time - timeActivation);
                //AudioManager.instance.PlaySFX(clip[0], audioSource);
                if (timeRemaining <= 0)
                {
                    PlateformOff();
                    maskStatus = MaskStatus.Empty;
                    cooldownActivation = Time.time;
                    ableToUse = false;
                }
                break;
            case MaskStatus.Empty:
                DeathPlayer.instance.Death();
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
        maskPlatforms = !maskPlatforms;
        foreach (PlatformVisibility platform in allPlatforms)
        {
            platform.ShowMaskPlatforms(maskPlatforms);
        }
        
    }

    public void PlateformOff()
    {
        maskPlatforms = !maskPlatforms;
        foreach (PlatformVisibility platform in allPlatforms)
        {
            platform.ShowMaskPlatforms(maskPlatforms);
        }
    }
    
    public void UseMask(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (ableToUse && !isInsidePlat)
            {
                if (maskStatus == MaskStatus.Full)
                {
                    PlateformOn();
                }
                else if (maskStatus == MaskStatus.Using)
                {
                    PlateformOff();
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Platform")
        {
            isInsidePlat = true;
        }
        else if(other.gameObject.tag == "Energy")
        {
            timeRemaining += energies;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Platform")
        {
            isInsidePlat = false;
        }
    }
}
