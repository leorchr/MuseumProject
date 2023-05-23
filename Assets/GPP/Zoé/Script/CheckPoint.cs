using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static CheckPoint instance;

    public GameObject spawn;
    [SerializeField] private Transform respawnPoint;


    private void Awake()
    {
        if (instance) Destroy(this);
        instance = this;
    }
    void Start()
    {
        //player instance le faire spawn au meme endroit que le gameobjet spawn
        respawnPoint = spawn.transform;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CheckPoint")
        {
            respawnPoint = other.GetComponent<Transform>();
            //changer l'aspect du checkpoint
            //mettre un sfx
        }
    }
}
