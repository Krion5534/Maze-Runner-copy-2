using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RandomWander : MonoBehaviour
{
    public float wanderRadius = 20f;
    public float wanderTimer = 5f;
    
    // Fixed: Changed name to match what you used below
    private Vector3 orgPosition; 
    private NavMeshAgent agent;
    private float timer;
    private Animator animator;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        orgPosition = transform.position; // Saves the starting position
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            // Check how far the agent has wandered from its original starting point
            float distanceFromOrigin = Vector3.Distance(transform.position, orgPosition);

            // If it's still within bounds, find a new random spot
            if (distanceFromOrigin <= wanderRadius)
            {
                Vector3 newPos = GetRandomNavSphere(transform.position, wanderRadius, NavMesh.AllAreas);
                agent.SetDestination(newPos);
                animator.SetTrigger("notSeeing");
            }
            // If it wandered too far, force it to walk back to the original starting point
            else
            {
                agent.SetDestination(orgPosition);
                animator.SetTrigger("notSeeing");
            }

            timer = 0; // Reset the timer
        }
    }

    // Fixed: Removed 'static' so it safely works with the script, and ensured it ALWAYS returns a Vector3
    public Vector3 GetRandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        // If it finds a valid spot on the NavMesh, return it
        if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            return navHit.position;
        }

        // Fallback: If it fails to find a point, just return the origin so the game doesn't crash
        return origin;
    }
}