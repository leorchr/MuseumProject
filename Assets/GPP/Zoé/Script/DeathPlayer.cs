using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayer : MonoBehaviour
{
    public static DeathPlayer instance;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }
    public void Death()
    {
        Mask.instance.timeRemaining = Mask.instance.duration;
        Mask.instance.maskStatus = MaskStatus.Full;
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
