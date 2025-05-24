using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //The enemy's health
    public int health = 100;
    
    //A prefab to spawn when the enemy dies
    public GameObject deathEffect;

    // A reference to the health bar
    private DisplayBar healthBar;
    
    // The damage the enemy deals to the player
    public int damage = 10;

    private void Start()
    {
        // Find the health bar in the children of the Enemy
        healthBar = GetComponentInChildren<DisplayBar>();

        if (healthBar == null)
        {
            // If the health bar is not found, log an error
            Debug.LogError("HealthBar (DisplayBar script) not found");
            return;
        }

        // Set the max value of the health bar to the enemy's health
        healthBar.SetMaxValue (health);
    }

    public void TakeDamage(int damage)
    {
        //Subtract the damage dealt from the health
        health -= damage;

        // Update the health bar
        healthBar.SetValue(health);

        //If the health is less than or equal to 0
        if(health <= 0)
        {
            //Call the Die function
            Die();
        }
    }

    void Die()
    {
        //Spawn a death effect
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        
        //Destroy the enemy
        Destroy(gameObject);
    }
    
    
    
    // Damage the player when the enemy collides with them
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player health script from the player object
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            
            // Check if the player health script is null
            if (playerHealth == null)
            {
                // Log an error if the player health script is null
                Debug.LogError("PlayerHealth script not found on player.");
                return;
            }
            
            // Damage the player
            playerHealth.TakeDamage(damage);
            
            // Knockback the player
            playerHealth.Knockback(transform.position);
            
        }
    }
    
    
}
