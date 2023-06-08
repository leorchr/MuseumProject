using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaskManager : MonoBehaviour
{
    private bool isActive = false;
    [SerializeField] private Mask mask;

    private void Start()
    {
        mask = GetComponent<Mask>();
        isActive = false;


        int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 3)
        {
            mask.enabled = true;
            MaskUI.instance.maskSlider.gameObject.SetActive(true);
        }
        else if (currentScene == 1 || currentScene == 2)
        {
            mask.enabled = false;
            MaskUI.instance.maskSlider.gameObject.SetActive(false);
        }
        else
        {
            mask.enabled = true;
            MaskUI.instance.maskSlider.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mask"))
        {
            if (!isActive)
            {
                mask.enabled = true;
                MaskUI.instance.maskSlider.gameObject.SetActive(true);
                isActive = true;
            }
        }
    }
}
