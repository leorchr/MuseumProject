using System.Collections;
using System.Collections.Generic;
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

    #endregion

    private void Start()
    {
        currentTarget = 0;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControler.instance.transform.position = PlayerControler.instance.respawnPosition;
        }
    }

    private void Update()
    {
        Movements();
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
        Vector2 dir = new Vector2(target.x - transform.position.x, 0);
        float angle = dir.x > 0f ? 180f : 0f;
        transform.rotation = new Quaternion(0, angle, 0, 0);
    }

    private void Movements()
    {
        if (target[currentTarget].transform.position.x - transform.position.x < 0.1f && target[currentTarget].transform.position.x - transform.position.x > -0.1f)
        {
            SelectNextTarget();
            //FlipSprite(target[currentTarget].transform.position);
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target[currentTarget].position.x, target[currentTarget].position.y, transform.position.z), speed * Time.deltaTime);
    }
}

