using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class ZombiePatrollingState : StateMachineBehaviour
{
    private float timer;
    public float patrollingTime = 10f; 
    private Transform player;
    private NavMeshAgent agent;
    public float detectionArea = 18f;
    public float patrolSpeed = 1.5f;

    private List<Transform> waypointsList = new List<Transform>();

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        
        agent.speed = patrolSpeed;
        timer = 0f;

        GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints");
        waypointsList.Clear();
        foreach (Transform child in waypointCluster.transform)
        {
            waypointsList.Add(child);
        }

        // Send navigation agent directly to its first randomized path target [00:10:39]
        if (waypointsList.Count > 0)
        {
            int randomIndex = Random.Range(0, waypointsList.Count);
            agent.SetDestination(waypointsList[randomIndex].position);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < detectionArea)
        {
            animator.SetBool("isChasing", true);
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            int randomIndex = Random.Range(0, waypointsList.Count);
            agent.SetDestination(waypointsList[randomIndex].position);
        }

       
        timer += Time.deltaTime;
        if (timer > patrollingTime)
        {
            animator.SetBool("isPatrolling", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Halt current velocity footprints when cutting off state nodes [00:12:08]
        agent.SetDestination(agent.transform.position);
    }
}