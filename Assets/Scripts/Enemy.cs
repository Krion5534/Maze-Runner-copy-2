using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class Enemy : MonoBehaviour
{
    // --- ENEMY BASIC STATE STATS ---
    public int HP = 100;             
    private Animator animator;         
    private NavMeshAgent navAgent;    

    void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }
    
    public void TakeDamage(int damage)
    {
        HP -= damage; 

        
        if (HP <= 0)
        {
            
            int randomDieAnimation = Random.Range(0, 2);

            if (randomDieAnimation == 0)
            {
                
                animator.SetTrigger("DIE1");
            }
            else
            {
                
                animator.SetTrigger("DIE2");
            }
        }
        else
        {
            
            animator.SetTrigger("DAMAGE");
            Debug.Log("Enemy hit! Remaining HP: " + HP);
        }
    }
    /*private void OnDrawGizmosSelected()
    {
        // 1. CHASE BOUNDARY (Blue Circle)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f); 

        // 2. ATTACK BOUNDARY (Red Circle)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f); 

        // 3. ESCAPE BOUNDARY (Green Circle)
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f); 
    }*/
}