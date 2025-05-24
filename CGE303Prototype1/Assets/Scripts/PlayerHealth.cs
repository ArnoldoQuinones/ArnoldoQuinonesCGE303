using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Variable to store the health of the player
    public int health = 100;

    // A reference to the health bar
    // This must be set in the inspector
    public DisplayBar healthBar;
    
    //reference to the Rigidbody2D of the player
    private Rigidbody2D rb;

    //Knockback force when the player collides with an enemy
    public float knockbackForce = 5f;

    // A prefab to spawn when the player dies
    // This must be set in the inspector
    public GameObject playerDeathEffect;

    // bool to keep track of if the player has been hit recently
    public static bool hitRecently = false;

    // time in seconds to recover from a hit
    public float hitRecoveryTime = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        //Set the Rigidbody2D reference
        rb = GetComponent<Rigidbody2D>();

        //check if the rb reference is null
        if (rb == null)
        {
            //Log an error message
            Debug.LogError("Rigidbody2D not found on player.");
        }

        //set the healthBar max balue to the player's health
        healthBar.SetMaxValue(health);

        // initialize hitRecently to false
        hitRecently = false;
        
        // set the AudioSource reference
        playerAudio = GetComponent<AudioSource>();
        
        //set the animator reference
        animator = GetComponent<Animator>();
    }

    // A function to knock the player back when they collide with an enemy
    public void Knockback(Vector3 enemyPosition)
    {
        // If the player has been hit recently
        if (hitRecently)
        {
            // Return out of the function
            return;
        }
        
        // Set hitRecently to true
        hitRecently = true;
        
        // Start the coroutine to reset hitRecently
        if (gameObject.activeSelf)
        {
            StartCoroutine(RecoverFromHit());
        }
        
        // Calculate the direction of the knockback
        Vector2 direction = transform.position - enemyPosition;
        
        // Normalize the direction vector
        // This gives a consistent knockback force regardless of the distance
        // between the player and the enemy
        direction.Normalize();

        // Add upward direction to the knockback
        direction.y = direction.y * 0.5f + 0.5f;
        
        // Add force to the player in the direction of the knockback
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        
    }
    
    // Coroutine to reset hitRecently after hitRecoveryTime seconds
    IEnumerator RecoverFromHit()
    {
        // Wait for hitRecovery Time (0.2) seconds
        yield return new WaitForSeconds(hitRecoveryTime);
    
        // Set hitRecently to false
        hitRecently = false;
        
        //set the hit animation to false
        animator.SetBool("hit", false);
    }
    
    //A function to take damage
    public void TakeDamage(int damage)
    {
    
        // Subtract the damage from the health
        health -= damage;
        
        // Update the health bar
        healthBar.SetValue(health);
        
        // TODO: Play a sound effect when the player takes damage
        // TODO: Play an animation when the player takes damage
        
        // If the health is less than or equal to 0
        if (health <= 0)
        {
            // Call the Die method
            Die();
        }
        else
        {
            // play the playerHit Sound
            playerAudio.PlayOneShot(playerHitSound);
            
            //play the player hit animation
            animator.SetBool("hit", true);
        }
    }
    
    // A function to die
    public void Die()
    {
        //set gameover to true
        ScoreManager.gameOver = true;
        
        //TODO: Play a sound effect when the player dies
        //TODO: Add the player death effect when the player dies
        
        // Instantiate the death effect at the player's position
        GameObject deathEffect = Instantiate(playerDeathEffect, transform.position, Quaternion.identity);
        
        // Destroy the death effect after 2 seconds
        Destroy(deathEffect, 2f);
        
        // Disable the player object
        gameObject.SetActive(false);
        
        //TODO:add sound effect for player death
    }
    
    private AudioSource playerAudio;
    public AudioClip playerHitSound;
    private Animator animator;
}
