using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // --- PLAYER STATS ---
    public int HP = 100;

   
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PLAYER WAS TOUCHED BY: " + other.gameObject.name);
        if (other.CompareTag("ZombieHand") || other.gameObject.name.Contains("Hand"))
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
            SceneChanger sceneChanger = GetComponentInChildren<SceneChanger>();
            if (sceneChanger != null)
            {
                sceneChanger.TriggerSceneLoad();
            }
            else
            {
                Debug.LogError("Could not find SceneChanger component in children!");
            }
        }
        else
        {
            
            print("Player hit! Remaining HP: " + HP);
        }
    }
}