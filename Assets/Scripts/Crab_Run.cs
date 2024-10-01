using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab_Run : StateMachineBehaviour
{
    [SerializeField] private Transform Player;
    private Rigidbody2D rb;
    [SerializeField] private float speed, minAttackDistance, attackLength;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        Player = animator.GetComponent<CrabBoss>().player.transform;
        speed = animator.GetComponent<CrabBoss>().speed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(Player.position.x, rb.position.y);
        Vector3 moveTowards = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(moveTowards);

        if (Vector2.Distance(rb.position, target) <= minAttackDistance)
        {
            animator.SetTrigger("Attack");
        }

        if (target.x > rb.position.x)
            animator.SetFloat("Move", 1f);
        else if(target.x < rb.position.x)
            animator.SetFloat("Move", -1f);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        rb.GetComponent<CrabBoss>().StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackLength);

        rb.GetComponent<CrabBoss>().Attack2();
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
