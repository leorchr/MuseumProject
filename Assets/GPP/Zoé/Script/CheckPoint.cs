using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static CheckPoint instance;

    public GameObject firstSpawn;
    [SerializeField] private Material checkPointDone;
    [HideInInspector] public Transform respawnPoint;


    private void Awake()
    {
        if (instance) Destroy(this);
        instance = this;
    }
    void Start()
    {
        //player instance le faire spawn au meme endroit que le gameobjet spawn
        respawnPoint = firstSpawn.transform;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckPoint")
        {
            Debug.Log("New Checkpoint set to : " + other.transform.position);
            respawnPoint = other.GetComponent<Transform>();
            other.GetComponent<MeshRenderer>().material = checkPointDone;
            //mettre un sfx
        }
    }
}
