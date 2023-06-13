using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    [SerializeField] private AudioClip step;
    [SerializeField] private AudioClip[] runSteps;
    [SerializeField] private AudioClip crouch;
    [SerializeField] private AudioSource source;

    public void StepSound()
    {
        source.volume = Random.Range(0.6f, 0.8f);
        source.pitch = Random.Range(1.2f, 1.6f);
        AudioManager.instance.PlaySFX(step, source, 0);
    }

    public void RunSound()
    {
        int random = Random.Range(0, runSteps.Length);
        source.volume = Random.Range(0.6f, 0.8f);
        source.pitch = Random.Range(0.8f, 1.2f);
        AudioManager.instance.PlaySFX(runSteps[random], source, 0);
    }

    public void Crouch()
    {
        AudioManager.instance.PlaySFX(crouch, source, 0.5f);
    }
}