using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChaseState : StateMachineBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    public float chaseSpeed = 4f;

    public float stopChasingDistance = 21f; 
    public float attackingDistance = 2.5f;   

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = chaseSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        agent.SetDestination(player.position);
        animator.transform.LookAt(player); 

        float distance = Vector3.Distance(player.position, animator.transform.position);

       
        if (distance < attackingDistance)
        {
            animator.SetBool("isAttacking", true);
        }

       
        if (distance > stopChasingDistance)
        {
            animator.SetBool("isChasing", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}