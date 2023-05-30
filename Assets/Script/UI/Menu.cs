using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu Instance;

    public GameObject Buttons;
    public GameObject OptionsWindow;

    private void Start()
    {
        if (Instance) Destroy(this);
        else Instance = this;

        Buttons.SetActive(true);
        OptionsWindow.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        EventSystem.current.SetSelectedGameObject(Buttons.transform.GetChild(0).gameObject);
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
        Buttons.SetActive(false);
        OptionsWindow.SetActive(true);
        EventSystem.current.SetSelectedGameObject(OptionsWindow.transform.GetChild(2).gameObject);
    }

    public void Back()
    {

        OptionsWindow.SetActive(false);
        Buttons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(Buttons.transform.GetChild(0).gameObject);
    }
}
