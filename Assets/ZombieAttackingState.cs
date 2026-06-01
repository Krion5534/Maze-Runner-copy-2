using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAttackState : StateMachineBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    public float stopAttackingDistance = 4f; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        LookAtPlayer(animator); 

        float distance = Vector3.Distance(player.position, animator.transform.position);
        
       
        if (distance > stopAttackingDistance)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    private void LookAtPlayer(Animator animator)
    {
        
        Vector3 direction = player.position - animator.transform.position;
        direction.y = 0f; 
        
        animator.transform.rotation = Quaternion.Slerp(
            animator.transform.rotation, 
            Quaternion.LookRotation(direction), 
            Time.deltaTime * 5f
        );
    }
}