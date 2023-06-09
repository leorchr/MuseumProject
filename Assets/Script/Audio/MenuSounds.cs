using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioClip menuNavigation;
    [SerializeField] private AudioClip buttonClick;
    private GameObject currentSelected;

    private void Start()
    {
        currentSelected = EventSystem.current.currentSelectedGameObject;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != currentSelected) NavigationSound();
        currentSelected = EventSystem.current.currentSelectedGameObject;
    }

    public void NavigationSound()
    {
        AudioManager.instance.PlaySFX(menuNavigation,audioSource);
    }

    public void ButtonClickSound()
    {
        AudioManager.instance.PlaySFX(buttonClick,audioSource2);
    }
}