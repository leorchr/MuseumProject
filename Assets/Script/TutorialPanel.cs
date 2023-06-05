using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialPanel : MonoBehaviour
{
    #region Main Parameters

    [Space]
    [Header("Parameters")]
    [Space]
    [Range(0f, 10f)][SerializeField] private float distance;
    [SerializeField] private Transform detection;

    [SerializeField] private GameObject panelKeyboard;
    [SerializeField] private GameObject panelController;

    #endregion

    #region Debug

    [Space]
    [Header("Debug")]
    [Space]
    [SerializeField] private bool visualDebugging = true;
    [SerializeField] private Color sphereColor = new Color(0f, 0f, 1f, 1f);

    #endregion

    private void Start()
    {
        panelController.SetActive(false);
        panelKeyboard.SetActive(false);
    }

    private void Update()
    {
        if(Vector3.Distance(PlayerController.instance.transform.position, detection.position) < distance)
        {
            SetControls();
        }
        else
        {
            panelController.SetActive(false);
            panelKeyboard.SetActive(false);
        }
    }

    private void SetControls()
    {
        if (PlayerController.instance.GetComponent<PlayerInput>().currentControlScheme == "Keyboard")
        {
            panelKeyboard.SetActive(true);
            panelController.SetActive(false);

        }
        else if (PlayerController.instance.GetComponent<PlayerInput>().currentControlScheme == "Controller")
        {
            panelController.SetActive(true);
            panelKeyboard.SetActive(false);
        }
    }


#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        if (!visualDebugging)
            return;
        Handles.color = sphereColor;
        if (visualDebugging)
        {
            Handles.RadiusHandle(Quaternion.identity, detection.position, distance);
        }
    }

#endif
}
