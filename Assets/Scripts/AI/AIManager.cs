using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIManager : MonoBehaviour
{
    
    NavMeshAgent agent;

    //Player information
    public GameObject player;
    Transform playerPos;
    MeshRenderer playerMesh;

    //Animator
    Animator managerAnimator;

    //Conditions
    bool playerSpotted;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        playerMesh = player.GetComponent<MeshRenderer>();
        playerPos = player.transform;

        managerAnimator = gameObject.GetComponent<Animator>();
    }

    public void MoveToPosition(Vector3 position)
    {
        agent.destination = position;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the manager can see the player
        if (playerMesh.isVisible)
        {
            Debug.DrawRay(transform.position, playerPos.position - transform.position, Color.red);

            RaycastHit hit;
            Physics.Raycast(transform.position, playerPos.position - transform.position, out hit);
             
            if (hit.transform == playerPos)
            {
                playerSpotted = true;
            }
        }

        //Triggers the animator
        if (playerSpotted)
            managerAnimator.SetBool("PlayerSpotted", true);
        else
            managerAnimator.SetBool("PlayerSpotted", false);

    }
}
