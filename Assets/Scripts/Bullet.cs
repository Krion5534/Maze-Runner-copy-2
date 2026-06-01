using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // The damage value is set dynamically by the weapon script upon spawning
    public int bulletDamage; 

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet hits a wall or standard environment
        if (collision.gameObject.CompareTag("Target"))
        {
            print("hit " + collision.gameObject.name + " !");
            Destroy(gameObject);
        }

       if (collision.gameObject.CompareTag("Wall"))
        {
            print("hit " + collision.gameObject.name + " !");
            Destroy(gameObject);
        }

        // Check if the hit object is tagged as a Zombie 
        if (collision.gameObject.CompareTag("Zombie"))
        {
            // Get the Zombie script component from the enemy object
            Enemy zombie = collision.gameObject.GetComponent<Enemy>();
            
            if (zombie != null)
            {
                // Inflict the dynamic damage amount 
                zombie.TakeDamage(bulletDamage);
            }

            // Destroy the bullet immediately after hitting the enemy
            Destroy(gameObject);
        }
    }
}