using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangingScene : MonoBehaviour
{
    public string targetScene;

    public void GoTo()
    {
        SceneManager.LoadScene(targetScene);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FadeInFadeOut.instance.FadeIn();
            Invoke("GoTo", FadeInFadeOut.instance.timeToFade - 1f);
        }
    }
}
