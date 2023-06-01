using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private PlayerControler playerControler;
    public static PlayerAnimation instance;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void isRunning()
    {
        if(playerControler.direction.x != 0 && !playerControler.isWallSliding)
        {
            //anim run
        }
        else
        {
            //anim idle
        }
    }

 

    public void IsSprinting()
    {
        //animator.SetBool("IsSprinting", true);
    }

    public void IsWallSliding()
    {
        if (playerControler.isWallSliding)
        {
            //anim wallslide = true;
        }
    }

    public void Idle()
    {
        if (playerControler.isGrounded)
        {
            //idle
           
        }
    }

    public void Falling()
    {
        //if (animator.GetBool("IsJumping"))
        //{
            //if (playerController.rb.velocity.y < 0)
            //{
                //animator.SetBool("IsJumping", false);
                //animator.SetBool("IsFalling", true);
            //}
       // }
    }

    public void wallSlideOn()
    {
        
        //wallSlide anim = true
        //isFalling = false
    }






}
