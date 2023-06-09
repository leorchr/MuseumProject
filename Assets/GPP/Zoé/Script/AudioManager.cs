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
        source.clip = clip;
        source.Play();
    }

    public void PlayRandomSFX(AudioClip[] clip, AudioSource source)
    {
        int i = Random.Range(0, clip.Length);
        source.clip = clip[i];
        source.Play();
    }
}
