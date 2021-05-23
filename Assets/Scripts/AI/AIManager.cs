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

    public Camera cam;

    public float hearingRange;

    //Animator
    Animator managerAnimator;

    //Conditions
    bool playerSpotted;

    //Waypoints
    public GameObject[] produceWP;
    public GameObject[] meatWP;
    public GameObject[] aisleWP;

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
        Vector3 viewportPos = cam.WorldToViewportPoint(player.transform.position);
        if (viewportPos.x < 1 && viewportPos.x > 0
            && viewportPos.y < 1 && viewportPos.y > 0
            && viewportPos.z > 0)
        {
            Debug.DrawRay(transform.position, playerPos.position - transform.position, Color.red);

            RaycastHit hit;
            if(Physics.Raycast(transform.position, playerPos.position - transform.position, out hit))
            {
                if(hit.collider.CompareTag("Detection"))
                {
                    playerSpotted = true;
                }
                else
                {
                    playerSpotted = false;
                }
            }
        }
        else
        {
            playerSpotted = false;
        }

        //Triggers the animator
        if (playerSpotted || 
            PlayerStealth.running || 
            (PlayerStealth.walking && Vector3.Distance(transform.position, playerPos.position) < hearingRange))
            managerAnimator.SetBool("PlayerSpotted", true);
        else
            managerAnimator.SetBool("PlayerSpotted", false);

    }
}
