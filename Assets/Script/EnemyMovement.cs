using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Enemy Movements

    [Space]
    [Header("Movements")]
    [Space]
    [SerializeField] private Transform[] target;
    private int currentTarget;
    
    [SerializeField] private float speed;
    //[SerializeField] private float smoothTime = 0.1f;
    //private Vector3 currentVelocity;

    #endregion

    private void Start()
    {
        currentTarget = 0;
    }

    private void Update()
    {
        if(target.Length > 1) Movements();
    }

    private void SelectNextTarget()
    {
        if (currentTarget < target.Length - 1)
        {
            currentTarget++;
        }
        else currentTarget = 0;
    }

    private void FlipSprite(Vector3 target)
    {
        Vector3 targetDirection = target - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, -Vector3.forward);
        transform.rotation = targetRotation;

    }

    private void Movements()
    {
        if (Vector3.Distance(target[currentTarget].transform.position, transform.position) < 0.1f && Vector3.Distance(target[currentTarget].transform.position, transform.position) > -0.1f)
        {
            SelectNextTarget();
            FlipSprite(target[currentTarget].transform.position);
        }
        //transform.position = Vector3.SmoothDamp(transform.position, target[currentTarget].position, ref currentVelocity, smoothTime);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target[currentTarget].position.x, target[currentTarget].position.y, transform.position.z), speed * Time.deltaTime);
    }
}

