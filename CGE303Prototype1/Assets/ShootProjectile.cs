using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this script to the player game object
//This script will allow the player to shoot projectiles
public class ShootProjectile : MonoBehaviour
{
    // Reference to the projectile prefab
    public GameObject projectilePrefab;

    // Reference to the firepoint transform
    // This is where the projectile will be instantiated
        // Make an empty child object of the player and
        // position it where you want the projectile to be fired from
        // and then assign it to this variable in the inspector
    public Transform firePoint;

    void Update()
    {
        //if the player presses the fire button,
        //call the shoot function
        if(Input.GetButtonDown("Fire1"))
        {
            //call the shoot function
            Shoot();
        }
    }

    void Shoot()
    {
        //instantiate the projectile at the firepoint position and rotation
        GameObject firedProjectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        //destroy the projectile after 3 seconds
        Destroy(firedProjectile, 3f);
    }
}
