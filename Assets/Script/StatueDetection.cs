using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class StatueDetection : MonoBehaviour
{
    [SerializeField] private GameObject statueEyes;

    private void Start()
    {
        statueEyes.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            statueEyes.SetActive(true);
            GetComponent<StatueDetection>().enabled = false;
        }
    }
}
