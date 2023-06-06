using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    private bool particlesStarted;

    private void Start()
    {
        particles.Play();
        particlesStarted = false;
    }
    private void Update()
    {
        ParticleSystem.MainModule mainModule = particles.main;
        switch (PlayerController.instance.playerStatus)
        {
            case PlayerStatus.Idle:
                particles.Stop();
                particlesStarted = false;
                break;
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
        }
    }
}
