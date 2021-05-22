using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : StateMachineBehaviour
{
    GameObject manager;
    GameObject player;

    AIManager managerManager;
    //Player script to get what section the player is in

    //All three sections of way points
    GameObject[] wayPointsProduce;
    GameObject[] wayPointsMeat;
    GameObject[] wayPointsAisle;


    //What section the player is in
    int currentSection = 0;
    GameObject[] currentSetOfWP;

    //Current waypoint
    int currentWP;

    //Timer for idle state
    float currentTime;
    float transitionTime;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        wayPointsProduce = GameObject.FindGameObjectsWithTag("WPProduce");
        wayPointsMeat = GameObject.FindGameObjectsWithTag("WPMeat");
        wayPointsAisle = GameObject.FindGameObjectsWithTag("WPAisle");

        currentWP = 0;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        manager = animator.gameObject;

        managerManager = manager.GetComponent<AIManager>();

        //Assigns the waypoints based on which section the player is in.
        switch (currentSection)
        {
            case 0:
                currentSetOfWP = wayPointsProduce;
                break;

            case 1:
                currentSetOfWP = wayPointsMeat;
                break;

            case 2:
                currentSetOfWP = wayPointsAisle;
                break;
        }

        managerManager.MoveToPosition(currentSetOfWP[currentWP].transform.position);

        currentTime = 0.0f;

        transitionTime = Random.Range(10.0f, 30.0f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Skip if there's no way points
        if (currentSetOfWP.Length == 0)
            return;

        //Transition to the next way point
        if(Vector3.Distance(currentSetOfWP[currentWP].transform.position, manager.transform.position) < 3.0f)
        {
            currentWP++;

            if (currentWP >= currentSetOfWP.Length)
                currentWP = 0;

            managerManager.MoveToPosition(currentSetOfWP[currentWP].transform.position);
        }

        //Vector3 direction = currentSetOfWP[currentWP].transform.position - manager.transform.position;
        //manager.transform.rotation = Quaternion.Slerp(manager.transform.rotation, Quaternion.LookRotation(direction), 1.0f * Time.deltaTime);

        if (currentTime >= transitionTime)
            animator.SetBool("IdleSpot", true);
        else
            currentTime += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}