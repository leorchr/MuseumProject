using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[Serializable]
public class ConfigData
{

    [Range(0f, 1f)] public float volume = 1f;

    public void Save()
    {
        int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 0)
        {
            volume = Menu.instance.MenuVolume;
        }
        else if (currentScene == 1)
        {
            volume = PauseMenu.instance.PauseVolume;
        }
    }

    public void Load()
    {
        int currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 0)
        {
            Menu.instance.MenuVolume = volume;
        }
        else if (currentScene == 1)
        {
            PauseMenu.instance.PauseVolume = volume;
            // appliquer le son
            //GlobalSound.instance.SetVolume(volume);
        }
    }
}
