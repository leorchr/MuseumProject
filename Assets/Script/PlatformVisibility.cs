using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVisibility : MonoBehaviour
{
    [SerializeField] private bool maskPlatform;
    [SerializeField] float speed = 1.0f;
    Material material;
    float currentAlpha = 0;
    int alphaTarget = 0;

    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<Renderer>().material;
        Color color = material.color;
        color.a = maskPlatform ? 1 : 0;
        currentAlpha = color.a;
        material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAlpha != alphaTarget)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, alphaTarget, speed * Time.deltaTime);
            Color color = material.color;
            color.a = currentAlpha;
            material.color = color;
        }
        GetComponent<Collider>().isTrigger = currentAlpha < 0.5f;
        
        
        
    }

    public void ShowMaskPlatforms(bool active)
    {
        if(active && maskPlatform)
        {
            alphaTarget = 1;
        }else if(!active && !maskPlatform)
        {
            alphaTarget = 1;
        }
        else
        {
            alphaTarget = 0;
        }
    }
}
