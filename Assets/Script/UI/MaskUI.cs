using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskUI : MonoBehaviour
{
    public static MaskUI instance;
    
    public Slider maskSlider;
    
    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    private void Start()
    {
        maskSlider.maxValue = Mask.instance.duration;
    }

    private void Update()
    {
        maskSlider.value = Mask.instance.timeRemaining;
    }
}
