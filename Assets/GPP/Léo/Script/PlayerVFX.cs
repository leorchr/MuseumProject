using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    public static PlayerVFX instance;

    [SerializeField] private ParticleSystem movementsParticles;
    private bool particlesStarted;
    private Vector3 particlesStartLocalPos;

    [SerializeField] private ParticleSystem deathParticles;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        particlesStarted = false;
        particlesStartLocalPos = movementsParticles.transform.localPosition;
    }
    private void Update()
    {
        MovementsParticles();
    }

    public void MovementsParticles()
    {
        ParticleSystem.MainModule mainModule = movementsParticles.main;
        switch (PlayerController.instance.playerStatus)
        {
            case PlayerStatus.Run:
                mainModule.startSpeed = 1f;
                if (!particlesStarted)
                {
                    SetUpMovementParticlesPos();
                    movementsParticles.Play();
                    particlesStarted = true;
                }
                break;
            case PlayerStatus.Sprint:
                mainModule.startSpeed = 2f;
                if (!particlesStarted)
                {
                    SetUpMovementParticlesPos();
                    movementsParticles.Play();
                    particlesStarted = true;
                }
                break;
            default:
                movementsParticles.gameObject.transform.parent = null;
                movementsParticles.Stop();
                particlesStarted = false;
                break;
        }
    }

    private void SetUpMovementParticlesPos()
    {
        movementsParticles.gameObject.transform.parent = gameObject.transform;
        movementsParticles.gameObject.transform.localPosition = particlesStartLocalPos;
        movementsParticles.gameObject.transform.rotation = PlayerController.instance.transform.rotation * Quaternion.Euler(0,180,0);
    }

    public void DeathParticles()
    {
        deathParticles.Play();
    }
}
