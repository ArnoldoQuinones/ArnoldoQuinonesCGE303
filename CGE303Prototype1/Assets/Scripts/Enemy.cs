using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //The enemy's health
    public int health = 100;
    
    //A prefab to spawn when the enemy dies
    public GameObject deathEffect;

    public void TakeDamage(int damage)
    {
        //Subtract the damage dealt from the health
        health -= damage;

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
}
