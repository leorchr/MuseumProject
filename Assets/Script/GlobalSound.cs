using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GlobalSound : MonoBehaviour
{
    public static GlobalSound instance;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;

    [SerializeField] private AudioSource audioSource;
    [Range(0f, 180f)][SerializeField] private float timeWanted;
     
    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        PlaySoundSpecificTime();
    }

    public void SetVolume()
    {
        float value = slider.value;
        if (value <= 0f) value = 0.001f;  
        audioMixer.SetFloat("Master", Mathf.Log10(value) * 20 * 1.5f);
    }

    private void PlaySoundSpecificTime()
    {
        audioSource.time = timeWanted;
        if(timeWanted > audioSource.clip.length)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.time = timeWanted;
            audioSource.Play();
        }
    }
}