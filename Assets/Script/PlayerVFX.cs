using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    public static PlayerVFX instance;

    [SerializeField] private ParticleSystem movementsParticles;
    private bool movementsStarted;
    private Vector3 movementsStartLocalPos;

    [SerializeField] private ParticleSystem slideParticles;
    private bool slideStarted;
    private Vector3 slideStartLocalPos;

    [SerializeField] private GameObject respawnParticles;
    [HideInInspector] public Transform respawnParticlesPos;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        movementsStarted = false;
        movementsStartLocalPos = movementsParticles.transform.localPosition;
        slideStarted = false;
        slideStartLocalPos = slideParticles.transform.localPosition;
    }
    private void Update()
    {
        MovementsParticles();
        SlideParticles();
    }

    #region Movements

    public void MovementsParticles()
    {
        ParticleSystem.MainModule mainModule = movementsParticles.main;
        switch (PlayerController.instance.playerStatus)
        {
            case PlayerStatus.Run:
                mainModule.startSpeed = 1f;
                if (!movementsStarted)
                {
                    SetUpMovementParticlesPos();
                    movementsParticles.Play();
                    movementsStarted = true;
                }
                break;
            case PlayerStatus.Sprint:
                mainModule.startSpeed = 2f;
                if (!movementsStarted)
                {
                    SetUpMovementParticlesPos();
                    movementsParticles.Play();
                    movementsStarted = true;
                }
                break;
            default:
                movementsParticles.gameObject.transform.parent = null;
                movementsParticles.Stop();
                movementsStarted = false;
                break;
        }
    }

    private void SetUpMovementParticlesPos()
    {
        movementsParticles.gameObject.transform.parent = gameObject.transform;
        movementsParticles.gameObject.transform.localPosition = movementsStartLocalPos;
        movementsParticles.gameObject.transform.rotation = PlayerController.instance.transform.rotation * Quaternion.Euler(0, 180, 0);
    }

    #endregion

    #region Wall Slide

    public void SlideParticles()
    {
        ParticleSystem.MainModule mainModule = movementsParticles.main;
        switch (PlayerController.instance.playerStatus)
        {
            case PlayerStatus.WallSlide:
                if (!slideStarted)
                {
                    SetUpSlideParticlesPos();
                    slideParticles.Play();
                    slideStarted = true;
                }
                break;
            default:
                slideParticles.gameObject.transform.parent = null;
                slideParticles.Stop();
                slideStarted = false;
                break;
        }
    }

    private void SetUpSlideParticlesPos()
    {
        slideParticles.gameObject.transform.parent = gameObject.transform;
        slideParticles.gameObject.transform.localPosition = slideStartLocalPos;
        slideParticles.gameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
    }

    #endregion

    public void DeathParticles()
    {
        Instantiate(respawnParticles, respawnParticlesPos.position, Quaternion.identity);
    }
}