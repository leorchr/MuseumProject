using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlayer : MonoBehaviour
{
    public static DeathPlayer instance;

    public float timeBetweenFade;
    public float timeToRespawn;
    public float speedFade;
    [HideInInspector] public bool isDead;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }
    public void Death()
    {
        isDead = true;
        Mask.instance.timeRemaining = Mask.instance.duration;
        Mask.instance.maskStatus = MaskStatus.Full;
        //FadeInFadeOut.instance.FadeIn(timeBetweenFade);
        //SFX
        Respawn();
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
        isDead = false;
    }
}
