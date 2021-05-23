using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MapSections 
{ 
    Produce = 1,
    Meat = 2,
    Aisle = 4
}

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
    MapSections currentSection;
    GameObject[] currentSetOfWP;

    //Current waypoint
    int currentWP;

    //Timer for idle state
    float currentTime;
    float transitionTime;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //wayPointsProduce = GameObject.FindGameObjectsWithTag("WPProduce");
        //wayPointsMeat = GameObject.FindGameObjectsWithTag("WPMeat");
        //wayPointsAisle = GameObject.FindGameObjectsWithTag("WPAisle");

        currentWP = 0;

        currentSection = MapSections.Produce;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        manager = animator.gameObject;

        managerManager = manager.GetComponent<AIManager>();

        wayPointsProduce = managerManager.produceWP;
        wayPointsMeat = managerManager.meatWP;
        wayPointsAisle = managerManager.aisleWP;

        CheckItems();

        //Assigns the waypoints based on which section the player is in.
        switch (currentSection)
        {
            case MapSections.Produce:
                currentSetOfWP = wayPointsProduce;
                break;

            case MapSections.Meat:
                currentSetOfWP = wayPointsMeat;
                break;

            case MapSections.Aisle:
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

        CheckItems();

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

    private void CheckItems()
    {
        if (Banana.dropped && currentSection == MapSections.Produce)
        {
            currentSection = MapSections.Meat;
            currentSetOfWP = wayPointsMeat;
            currentWP = 0;
        }
        if (Pork.dropped && currentSection == MapSections.Meat)
        {
            currentSection = MapSections.Aisle;
            currentSetOfWP = wayPointsAisle;
            currentWP = 0;
        }
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
