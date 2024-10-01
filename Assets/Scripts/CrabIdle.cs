using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabIdle : StateMachineBehaviour
{
    float time, timeToNextJump;
    [SerializeField] private float min, max;
    [SerializeField] private CrabBoss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
        timeToNextJump = Random.Range(min, max);
        boss = animator.GetComponent<CrabBoss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.timeScale == 0) return;

        time += Time.deltaTime;

        if(time >= timeToNextJump)
        {
            timeToNextJump = Random.Range(min, max);
            time = 0;
            animator.SetTrigger("Attack");
            boss.Attack1();
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
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
