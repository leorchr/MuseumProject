using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class StatueDetection : MonoBehaviour
{
    [SerializeField] private GameObject statueEyes;

    [Header("SFX")]
    public AudioClip audioClip;
    public AudioSource audioSource;

    private void Start()
    {
        statueEyes.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            statueEyes.SetActive(true);
            AudioManager.instance.PlaySFX(audioClip, audioSource, 1f);
            Destroy(this);
        }
    }
}