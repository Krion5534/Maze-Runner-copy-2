using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : StateMachineBehaviour
{
    private float timer;
    public float idleTime = 4f; 
    private Transform player;
    public float detectionAreaRadius = 18f; 

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < detectionAreaRadius)
        {
            animator.SetBool("isChasing", true);
        }

        
        timer += Time.deltaTime;
        if (timer > idleTime)
        {
            animator.SetBool("isPatrolling", true);
        }
    }
}