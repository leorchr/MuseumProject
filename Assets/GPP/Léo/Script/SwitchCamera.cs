using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private GameObject cam1, cam2;

    private void OnTriggerEnter(Collider other)
    {
        cam2.SetActive(true);
        cam1.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        cam2.SetActive(false);
        cam1.SetActive(true);
    }
}
