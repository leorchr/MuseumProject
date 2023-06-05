using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayer : MonoBehaviour
{
    public float timeToRespawn;
    public void Death()
    {
        Invoke("Respawn", timeToRespawn);
        Mask.instance.timeRemaining = Mask.instance.duration;
        FadeInFadeOut.instance.FadeIn();
        //SFX

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Void" || other.gameObject.tag == "Enemy")
        {
            Death();
        }
    }

    public void Respawn()
    {
        PlayerController.instance.GetComponent<Rigidbody>().position = PlayerController.instance.respawnPosition;
    }
}
