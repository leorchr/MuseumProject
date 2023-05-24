using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Enemy Movements

    public Transform[] target;
    private int currentTarget;
    [SerializeField] private float speed;

    #endregion

    #region Player Detection

    private RaycastHit hit;
    [SerializeField] private Transform raycastPos;
    [Range(1f, 50f)][SerializeField] private float viewingDistance = 5f;
    [SerializeField] private LayerMask playerMask = ~0;
    [SerializeField] private LayerMask obstacleMask = ~0;
    private bool playerFocus;

    #endregion

    #region Debug
    [Space]
    [Header("Debug")]
    [Space]
    [SerializeField] private bool visualDebugging = true;
    [SerializeField] private Color color = new Color(0.75f, 0.2f, 0.2f, 0.75f);
    [Range(1f, 10f)][SerializeField] private float lineWidth = 5f;

    #endregion

    private void Start()
    {
        currentTarget = 0;
        playerFocus = false;
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
        DetectPlayer();
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
        // Deplacement focus joueur
        if (playerFocus)
        {
            FlipSprite(PlayerControler.instance.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(PlayerControler.instance.transform.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
        // Deplacement standard entre deux points
        else
        {
            //if (Vector3.Distance(transform.position, target[currentTarget].transform.position) < 0.1f) // suit la position exacte
            if (target[currentTarget].transform.position.x - transform.position.x < 0.1f && target[currentTarget].transform.position.x - transform.position.x > -0.1f) // suit l'axe x
            {
                SelectNextTarget();
            }
            FlipSprite(target[currentTarget].transform.position);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target[currentTarget].position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        }
    }

    private void DetectPlayer()
    {
        //if ((PlayerControler.instance.transform.position.x - transform.position.x < 0.1f && PlayerControler.instance.transform.position.x - transform.position.x > -0.1f))
        if (Physics.Raycast(raycastPos.position, -raycastPos.transform.right, out hit, viewingDistance, playerMask))
        {
            float distObject = Vector3.Distance(hit.transform.position, transform.position);
            if (!Physics.Raycast(raycastPos.position, -raycastPos.transform.right, distObject, obstacleMask))
            {
                playerFocus = true;
                Debug.Log("Je vois le joueur !");
            }

        }
        else
        {
            playerFocus = false;
            Debug.Log("Je ne vois pas le joueur");
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        if (!visualDebugging)
            return;

        Handles.color = color;
        Handles.DrawAAPolyLine(lineWidth, raycastPos.position, raycastPos.position + -raycastPos.transform.right * viewingDistance);
    }
#endif
}

