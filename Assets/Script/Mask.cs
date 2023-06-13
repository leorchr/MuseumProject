using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevel;

public enum MaskStatus
{
    Full, Using, Empty, Charging
}

public class Mask : MonoBehaviour
{
    public static Mask instance;

    public float duration;
    [SerializeField] private float multiplicateurDecharging = 1f;
    [SerializeField] private float multiplicateurRecharging = 1f;
    [HideInInspector] public float timeRemaining;
    private bool isInsidePlat;

    [SerializeField] private float cooldown;
    private float cooldownRemaining;
    private float cooldownActivation;
    private bool ableToUse;
    public float playerSpeedReduction;

    [HideInInspector] public MaskStatus maskStatus = MaskStatus.Full;

    public float addEnergy;

    /*[Header("Sound")]
    public AudioSource audioSource;
    public AudioClip[] clip;*/

    public PlatformVisibility[] allPlatforms;
    bool maskPlatforms = true;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        allPlatforms = FindObjectsOfType<PlatformVisibility>();
        foreach (PlatformVisibility platform in allPlatforms)
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
                timeRemaining -= Time.deltaTime * multiplicateurDecharging;
                //AudioManager.instance.PlaySFX(clip[0], audioSource);
                if (timeRemaining <= 0)
                {
                    timeRemaining = 0;
                    PlateformOff();
                    maskStatus = MaskStatus.Empty;
                    cooldownActivation = Time.time;
                    ableToUse = false;
                }
                break;
            case MaskStatus.Empty:
                timeRemaining = 0;
                cooldownRemaining = cooldown - (Time.time - cooldownActivation);
                PlayerController.instance.sprintSpeed = playerSpeedReduction;
                PlayerController.instance.walkSpeed = playerSpeedReduction;
                PlayerController.instance.moveSpeed = PlayerController.instance.walkSpeed;
                if (cooldownRemaining <= 0)
                {
                    maskStatus = MaskStatus.Charging;
                    ableToUse = true;
                    PlayerController.instance.sprintSpeed = 8f;
                    PlayerController.instance.walkSpeed = 4.5f;
                    PlayerController.instance.moveSpeed = PlayerController.instance.walkSpeed;
                }
                break;
            case MaskStatus.Charging:
                timeRemaining += Time.deltaTime * multiplicateurRecharging;
                if (timeRemaining >= duration)
                {
                    timeRemaining = duration;
                    maskStatus = MaskStatus.Full;
                }
                break;
            case MaskStatus.Full:
                break;
        }
    }

    public void PlateformOn()
    {
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

    public void ResetPlatforms()
    {
        timeRemaining = duration;
        maskStatus = MaskStatus.Full;
        foreach (PlatformVisibility platform in allPlatforms)
        {
            platform.ResetMaskPlatforms();
        }
        maskPlatforms = true;
        ableToUse = true;
    }

    public void UseMask(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (ableToUse && !isInsidePlat)
            {
                if (maskStatus == MaskStatus.Full || maskStatus == MaskStatus.Charging)
                {
                    PlateformOn();
                }
                else if (maskStatus == MaskStatus.Using)
                {
                    PlateformOff();
                    maskStatus = MaskStatus.Charging;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Platform")
        {
            isInsidePlat = true;
        }
        if (other.gameObject.tag == "Energy")
        {
            PlusEnergy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Platform")
        {
            isInsidePlat = false;
        }
    }

    private void PlusEnergy(GameObject energy)
    {
        if(maskStatus == MaskStatus.Empty)
        {
            maskStatus = MaskStatus.Charging;
            ableToUse = true;
            PlayerController.instance.sprintSpeed = 8f;
            PlayerController.instance.walkSpeed = 4.5f;
            PlayerController.instance.moveSpeed = PlayerController.instance.walkSpeed;
        }
        timeRemaining += addEnergy;
        if (timeRemaining > duration)
        {
            timeRemaining = duration;
        }
        energy.SetActive(false);
    }
}
