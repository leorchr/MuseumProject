using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayer : MonoBehaviour
{
    public static DeathPlayer instance;
    private Mask mask;
    public bool isDead;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        mask = GetComponent<Mask>();
        isDead = false;
    }
    public void Death()
    {
        Mask.instance.cooldownRemaining = 0;
        isDead = true;
        Mask.instance.timeRemaining = Mask.instance.duration;
        if (Mask.instance.allPlatforms.Length == 0)
        {
            mask.ResetPlatforms();
        }

        PlayerController.instance.sprintSpeed = 0;
        PlayerController.instance.walkSpeed = 0;
        PlayerController.instance.moveSpeed = PlayerController.instance.walkSpeed;
        FadeDeath.instance.FadeIn();
        //SFX
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Void" || other.gameObject.tag == "Enemy")
        {
            Debug.Log("touché");
            Death();
        }
    }

    public void Respawn()
    {
        PlayerController.instance.GetComponent<Rigidbody>().position = PlayerController.instance.respawnPosition;
        PlayerVFX.instance.DeathParticles();
        PlayerController.instance.sprintSpeed = 8f;
        PlayerController.instance.walkSpeed = 4.5f;
        PlayerController.instance.moveSpeed = PlayerController.instance.walkSpeed;
        isDead = false;
    }
}
