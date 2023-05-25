using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskUI : MonoBehaviour
{
    [SerializeField] private Slider maskSlider;

    private void Start()
    {
        maskSlider.maxValue = Mask.instance.duration;
    }

    private void Update()
    {
        maskSlider.value = Mask.instance.timeRemaining;
    }
}
