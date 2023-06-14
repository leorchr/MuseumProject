using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySounds : MonoBehaviour
{
    public static PlaySounds instance;

    private void Awake()
    {
        if (instance != null) Destroy(this);
        else instance = this;
    }

    [SerializeField] private AudioClip step;
    [SerializeField] private AudioClip[] runSteps;
    [SerializeField] private AudioClip crouch;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioSource source;

    [Space]
    [SerializeField] private AudioClip maskOn;
    [SerializeField] private AudioClip maskOff;
    [SerializeField] private AudioSource sourceMask;

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
        source.volume = 1;
        source.pitch = 1;
        AudioManager.instance.PlaySFX(crouch, source, 0.5f);
    }
    
    public void Jump()
    {
        source.volume = 0.1f;
        source.pitch = Random.Range(0.9f, 1.1f);
        AudioManager.instance.PlaySFX(jump, source,0,0.3f);
    }

    public void MaskOn()
    {
        AudioManager.instance.PlaySFX(maskOn, sourceMask, 0.4f);
    }

    public void MaskOff()
    {
        AudioManager.instance.PlaySFX(maskOff, sourceMask, 0.4f);
    }
}