using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Mask : MonoBehaviour
{

    [Space]
    [Header("Plateform\n--------------")]
    public bool isActivate;
    public GameObject[] plateforms;
    int length = 0;

    public Material material;
    private float timeToFade = 3f;
    private float matAlpha;

    public Slider slider;
    private float timeActivation;
    private float duration = 1f;

    public float timeSlider;

    void Start()
    {
        PlateformOff();
        length = plateforms.Length;
    }

    void Update()
    {
        Debug.Log("Time Activation = " + timeActivation);
        if (isActivate)
        {
            //timeSlider = duration - Time.time;
            timeSlider = duration - (Time.time - timeActivation);
            slider.value = timeSlider;
            if (timeActivation + duration < Time.time)
            {
                Debug.Log("frefkelmrk");
                PlateformOff();
            }
        }
    }
    


    public void PlateformOn()
    {
        isActivate = true;

        for (int i = 0; i < length; i++)
        {
            
            plateforms[i].SetActive(true);
        }
        //material.color = new Color(0.5f, 0.7f, 0.8f, 1f);
    }

    public void PlateformOff()
    {

        isActivate = false;
        //material.color = new Color(0.5f, 0.7f, 0.8f, -1f); 
        for (int i = 0; i < length; i++)
        {
            plateforms[i].SetActive(false);
        }
        //material.color = new Color(0.5f, 0.7f, 0.8f, -1f);
    }

    public void UseMask(InputAction.CallbackContext context)
    {
        //matAlpha = Time.time * timeToFade;
        timeActivation = Time.time;
        PlateformOn();
    }
}

