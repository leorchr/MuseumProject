using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private GameObject cam1, cam2;

    private void Start()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }
    }
}
