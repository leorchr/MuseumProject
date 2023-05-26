using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : MonoBehaviour
{




    
 
    void Start()
    {
       
    }

 
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wallSlide"))
        {
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("wallSlide"))
        {
            //Cancel anim
        }
    }
}
