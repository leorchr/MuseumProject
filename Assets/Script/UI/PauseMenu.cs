using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject resume;
    public GameObject volumeSlider;

    public float PauseVolume
    {
        get { return volumeSlider.GetComponent<Slider>().value; }
        set { volumeSlider.GetComponent<Slider>().value = value; }
    }

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    void Paused()
    {
        pauseMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(resume);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }


    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene("Title Screen");
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}