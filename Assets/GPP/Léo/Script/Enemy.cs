using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] target;
    private int currentTarget;
    [SerializeField] private float speed;

    [SerializeField] private bool playerFocus;

    //temporaire
    public float distanceFocus = 5f;

    private void Start()
    {
        currentTarget = 0;
        playerFocus = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControler.instance.transform.position = PlayerControler.instance.respawnPosition;
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerControler.instance.transform.position) > distanceFocus || Vector3.Distance(transform.position, target[0].position) > distanceFocus)
        {
            playerFocus = false;
        }
        else playerFocus = true;


        if (playerFocus)
        {
            // Deplacement focus joueur
            Debug.Log("Là je suis entrain de suivre le joueur frérot");
            transform.position = Vector3.MoveTowards(transform.position, PlayerControler.instance.transform.position, speed * Time.deltaTime);
            
        }
        else
        {
            if (Vector3.Distance(transform.position, target[currentTarget].transform.position) < 0.1f)
            {
                SelectNextTarget();
            }
            // Deplacement standard entre deux points
            Debug.Log("Là je t'explique je suis entrain de me diriger vers la prochaine position qui est en : " + target[currentTarget].position);
            transform.position = Vector3.MoveTowards(transform.position, target[currentTarget].position, speed * Time.deltaTime);
        }
    }

    private void SelectNextTarget()
    {
        if (currentTarget < target.Length-1)
        {
            currentTarget++;
        }
        else currentTarget = 0;
        Debug.Log(currentTarget);
    }
}
