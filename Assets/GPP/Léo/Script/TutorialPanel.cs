using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    #region Main Parameters

    [Space]
    [Header("Parameters")]
    [Space]
    [Range(0f, 10f)][SerializeField] private float distance;
    [SerializeField] private Transform detection;
    private bool check;
    [SerializeField] private GameObject panel;

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
        check = false;
    }

    private void Update()
    {
        if(Vector3.Distance(PlayerControler.instance.transform.position, detection.position) < distance)
        {
            check = true;
            panel.SetActive(true);
        }
        else if (check)
        {
            panel.SetActive(false);
            gameObject.GetComponent<TutorialPanel>().enabled = false;
        }
        else panel.SetActive(false);
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
