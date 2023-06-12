using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioSource source;

    public void StepSound()
    {
        source.volume = Random.Range(0.6f, 0.8f);
        source.pitch = Random.Range(0.8f, 1.2f);
        AudioManager.instance.PlaySFX(sounds[0], source, 0, 4);
    }
}