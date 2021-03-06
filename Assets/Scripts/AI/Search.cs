using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search : StateMachineBehaviour
{
    GameObject manager;
    AIManager managerManager;

    GameObject player;

    Vector3 lastPos;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Searching", true);

        manager = animator.gameObject;
        managerManager = manager.GetComponent<AIManager>();

        managerManager.PlaySound(Sounds.Search);

        lastPos = player.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Moves manager to player's last known position
        managerManager.MoveToPosition(lastPos);

        if (Vector3.Distance(lastPos, manager.transform.position) < 3.0f)
            animator.SetBool("Searching", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IdleSpot", true);
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
