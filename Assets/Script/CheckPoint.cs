using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Material materialDone;
    [SerializeField] private MeshRenderer startMesh;
    [SerializeField] private bool onlyOnce = false;
    private bool check = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !check)
        {
            //Debug.Log("New Checkpoint set to : " + transform.position);
            PlayerControler.instance.respawnPosition = transform.position;
            startMesh.material = materialDone;
            //mettre un sfx
            if (onlyOnce) check = true;
        }
    }
}