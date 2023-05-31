using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject Buttons;

    private void Start()
    {
        pauseMenuUI.SetActive(false);

        
    }

    void Paused()
    {
        pauseMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(Buttons);
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
        SceneManager.LoadScene("Menu");
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