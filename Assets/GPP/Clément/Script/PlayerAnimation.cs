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

        }
    }




}
