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

    /// <summary>
    /// Cette fonction joue un son à un moment spécifique
    /// </summary>
    public void PlaySFX(AudioClip clip, AudioSource source, float startTime = 0f, float volume = 1f)
    {
        source.clip = clip;
        source.volume = volume;
        if (startTime > source.clip.length)
        {
            Debug.LogWarning("Le son est plus court que " + startTime + " secondes");
            source.Play();
        }
        else
        {
            source.time = startTime;
            source.Play();
        }
    }

    public void PlayRandomSFX(AudioClip[] clip, AudioSource source)
    {
        int i = Random.Range(0, clip.Length);
        source.clip = clip[i];
        source.Play();
    }
}
