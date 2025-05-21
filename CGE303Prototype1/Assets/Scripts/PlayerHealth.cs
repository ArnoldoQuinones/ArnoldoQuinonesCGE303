using System.Collections;
using System.Collections.Generic;
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
    public fload knockbackForce = 5f;

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

            //Log an error message
            Debut.LogError("Rigidbody2D not found on player.")

            //set the healthBar max balue to the player's health
            healthBar.SetMaxValue(health);

            // initialize hitRecently to false
            hitRecently = false;
    }

    // A function to knock the player when they collide

    // Update is called once per frame
    void Update()
    {
        
    }
}
