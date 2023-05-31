using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator animator;
    [SerializeField] private PlayerControler playerControler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isRunning()
    {
        if(playerControler.direction.x != 0)
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
            //if (animator.GetBool("isFalling"))
            //{
               // animator.SetBool("isFalling", false);
            //}
        }
    }

    //public void Falling()
    //{
        //if (_animator.GetBool("IsJumping"))
        //{
            //if (_playerController.rb.velocity.y < 0)
            //{
                //_animator.SetBool("IsJumping", false);
                //_animator.SetBool("IsFalling", true);
            //}
       // }
    //}

    public void wallSlideOn()
    {
        //wallSlide anim = true
        //isFalling = false
    }




}
