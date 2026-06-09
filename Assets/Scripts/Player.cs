using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // --- PLAYER STATS ---
    public int maxHP = 100;
    public int HP;
    public HealthBar healthBar;

    private void OnTriggerEnter1(Collider other)
    {
        // Your log proved this line runs successfully:
        Debug.Log("PLAYER WAS TOUCHED BY: " + other.gameObject.name);

        // 2. Check if the object we touched is named "end"
        if (other.gameObject.name == "end")
        {
            // 3. Load the scene (Replace "YourSceneName" with your actual scene's name)
            SceneManager.LoadScene("YourSceneName"); 
        }
    }
    private void Start()
    {
        HP = maxHP;
        healthBar.SetMaxHealth(maxHP);
    }
    
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
        healthBar.SetHealth(HP); 

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