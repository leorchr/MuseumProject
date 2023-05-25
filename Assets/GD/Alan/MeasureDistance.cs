using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeasureDistance : MonoBehaviour
{
    void Start()
    {
        Transform cameraTransform = Camera.main.transform;
        Transform characterTransform = GameObject.Find("Player").transform;
        float distance = Vector3.Distance(cameraTransform.position, characterTransform.position);
        Debug.Log("Distance between camera and character: " + distance);
    }
}
