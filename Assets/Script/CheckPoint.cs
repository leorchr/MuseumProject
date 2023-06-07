using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private MeshRenderer startMesh;
    [SerializeField] private Material doneMaterial;

    [SerializeField] private MeshRenderer signMesh;
    [SerializeField] private Material signMaterial;


    [SerializeField] private bool onlyOnce = false;
    private bool check = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !check)
        {
            //Debug.Log("New Checkpoint set to : " + transform.position);
            PlayerController.instance.respawnPosition = transform.position;
            startMesh.material = doneMaterial;
            signMesh.material = signMaterial;
            //mettre un sfx
            if (onlyOnce) check = true;
        }
    }
}