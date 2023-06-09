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
                Debug.Log("Idle");
                animator.SetTrigger("Idle");
                break;
            case PlayerStatus.Run:
                Debug.Log("Run");
                animator.SetTrigger("Run");
                break;

            case PlayerStatus.Sprint:


                break;

            case PlayerStatus.WallSlide:

                break;

            case PlayerStatus.Fall:
                
                break;

            case PlayerStatus.Crouch:

                break;
            case PlayerStatus.CrouchRun:

                break;
           

        }
    }

  






}
