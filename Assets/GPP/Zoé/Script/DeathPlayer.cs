using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayer : MonoBehaviour
{
    public static DeathPlayer instance;
    private Mask mask;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        mask = GetComponent<Mask>();
    }
    public void Death()
    {
        if (mask.enabled)
        {
            Mask.instance.PlateformOff();
            Mask.instance.timeRemaining = Mask.instance.duration;
            Mask.instance.maskStatus = MaskStatus.Full;
        }
        //bloqué les mouvements du joueur
        FadeDeath.instance.FadeIn();
        //SFX
        //Respawn();
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
    }
}
