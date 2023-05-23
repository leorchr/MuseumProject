using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //go to spawn point
            PlayerControler.instance.transform.position = CheckPoint.instance.respawnPoint.position;
        }
    }
}
