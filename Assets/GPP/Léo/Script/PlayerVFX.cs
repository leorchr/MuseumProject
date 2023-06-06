using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    public static PlayerVFX instance;

    [SerializeField] private ParticleSystem particles;
    private bool particlesStarted;

    [SerializeField] private ParticleSystem deathParticles;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        particlesStarted = false;
    }
    private void Update()
    {
        ParticleSystem.MainModule mainModule = particles.main;
        switch (PlayerController.instance.playerStatus)
        {
            case PlayerStatus.Run:
                mainModule.startSpeed = 1f;
                if (!particlesStarted)
                {
                    particles.Play();
                    particlesStarted = true;
                }
                break;
            case PlayerStatus.Sprint:
                mainModule.startSpeed = 2f;
                if (!particlesStarted)
                {
                    particles.Play();
                    particlesStarted = true;
                }
                break;
            default:
                particles.Stop();
                particlesStarted = false;
                break;
        }
    }

    public void DeathParticles()
    {
        deathParticles.Play();
    }
}
