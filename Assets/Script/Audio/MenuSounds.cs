using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip menuNavigation;
    [SerializeField] private AudioClip buttonClick;
    private GameObject currentSelected;
    private bool DoNotPlayNavigation;

    private void Start()
    {
        DoNotPlayNavigation = false;
        currentSelected = EventSystem.current.currentSelectedGameObject;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != currentSelected && !DoNotPlayNavigation) NavigationSound();
        currentSelected = EventSystem.current.currentSelectedGameObject;
        DoNotPlayNavigation = false;
    }

    public void NavigationSound()
    {
        AudioManager.instance.PlaySFX(menuNavigation,audioSource);
    }

    public void ButtonClickSound()
    {
        DoNotPlayNavigation = true;
        AudioManager.instance.PlaySFX(buttonClick,audioSource);
    }
}