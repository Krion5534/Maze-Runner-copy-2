using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // --- PLAYER STATS ---
    public int HP = 100;

   
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("ZombieHand"))
        {
            
            ZombieHand handScript = other.gameObject.GetComponent<ZombieHand>();

            if (handScript != null)
            {
                
                TakeDamage(handScript.damage);
            }
        }
    }

    
    public void TakeDamage(int damage)
    {
        HP -= damage; 

        if (HP <= 0)
        {
            
            print("Player is Dead!"); 
        }
        else
        {
            
            print("Player hit! Remaining HP: " + HP);
        }
    }
}