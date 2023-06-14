using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private MeshRenderer startMesh;
    [SerializeField] private Material doneMaterial;

    [SerializeField] private MeshRenderer signMesh;
    [SerializeField] private Material signMaterial;

    [SerializeField] private Transform respawnPos;
    [SerializeField] private Transform respawnVfxPos;

    public AudioSource audioSource;
    public AudioClip audioClip;

    [SerializeField] private bool onlyOnce = false;
    private bool check = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !check)
        {
            //Debug.Log("New Checkpoint set to : " + transform.position);
            PlayerController.instance.respawnPosition = respawnPos.position;
            PlayerVFX.instance.respawnParticlesPos = respawnVfxPos;
            startMesh.material = doneMaterial;
            signMesh.material = signMaterial;
            if(GetComponent<MeshRenderer>() != null)
            {
                AudioManager.instance.PlaySFX(audioClip, audioSource);
            }
            if (onlyOnce) check = true;
        }
    }
}