using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu instance;

    public GameObject buttons;
    public GameObject settingsWindow;
    public GameObject volumeSlider;
    public float MenuVolume
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
        buttons.SetActive(true);
        settingsWindow.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        EventSystem.current.SetSelectedGameObject(buttons.transform.GetChild(0).gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

	public void Options()
    {
        buttons.SetActive(false);
        settingsWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingsWindow.transform.GetChild(2).gameObject);
    }

    public void Back()
    {

        settingsWindow.SetActive(false);
        buttons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(buttons.transform.GetChild(0).gameObject);
    }
}
