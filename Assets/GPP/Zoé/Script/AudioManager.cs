using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    public void PlaySFX(AudioClip clip, AudioSource source)
    {
        Debug.Log("musique");
        source.clip = clip;
        source.Play();
    }
}
