using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangingScene : MonoBehaviour
{
    public static ChangingScene instance;

    public string targetScene;

    public float timeBetweenFade;
    public float speedFade;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }
    public void GoTo()
    {
        SceneManager.LoadScene(targetScene);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FadeChangingScene.instance.FadeIn();
        }
    }
}
