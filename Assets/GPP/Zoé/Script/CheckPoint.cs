using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject spawn;
    [SerializeField] private Transform respawnPoint;

    void Start()
    {
        respawnPoint = spawn.transform;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckPoint")
        {
            respawnPoint = other.GetComponent<Transform>();
        }
    }
}
