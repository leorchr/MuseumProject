using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private PlayerController playerControler;
    public static PlayerAnimation instance;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        switch (playerControler.playerStatus)
        {
            case PlayerStatus.Idle:
                
                animator.SetTrigger("Idle");
                break;
            case PlayerStatus.Run:
                
                animator.SetTrigger("Run");
                break;

            case PlayerStatus.Sprint:
                animator.SetTrigger("Sprint");
                break;

            case PlayerStatus.WallSlide:
                animator.SetTrigger("WallSlide");
                break;
            case PlayerStatus.Fall:
                animator.SetTrigger("Fall");
                break;
            case PlayerStatus.Jump:
                animator.SetTrigger("Jump");
                break;

            case PlayerStatus.Crouch:
                animator.SetTrigger("Crouch");
                break;
            case PlayerStatus.CrouchRun:
                animator.SetTrigger("Crouch Run");
                break;
           

        }
    }

  






}
