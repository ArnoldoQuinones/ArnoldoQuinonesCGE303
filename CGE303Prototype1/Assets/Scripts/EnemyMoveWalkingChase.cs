using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

// Require a Rigidbody2D and an Animator on the enemy
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemyMoveWalkingChase : MonoBehaviour
{
    // Range at which the enemy will chase the player
    public float chaseRange = 4f;
    
    // Speed of the enemy movement
    public float enemyMovementSpeed = 1.5f;
    
    // Transform of the player object
    private Transform playerTransform;
    
    // Rigidbody component of the enemy
    private Rigidbody2D rb;
    
    // Animator component of the enemy
    private Animator anim;
    
    //SpriteRenderer of the enemy
    private SpriteRenderer sr;

    void Start()
    {
        // Get the Rigidbody2D component of the enemy
        rb = GetComponent<Rigidbody2D>();
        
        // Get the Animator component of the enemy
        anim = GetComponent<Animator>();
        
        // Get the player transform using the "Player" tag
        playerTransform = GameObject.FindWithTag("Player").transform;
        
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // This Vector2 is a 2D arrow from the enemy to the player
        Vector2 playerDirection = playerTransform.position - transform.position;
        
        // Distance between the enemy and the player
        // The magnitude of the vector is the distance without the direction
        float distanceToPlayer = playerDirection.magnitude;
        
        // Check if the player is within chase range
        if (distanceToPlayer <= chaseRange)
        {
            // We need the direction to the player on only the x asis
            
            // Normalize gives us the direction to the player without the distance
            playerDirection.Normalize();
            
            // Setting the y axis to 0 because we only want to move on the x axis
            playerDirection.y = 0f;
            
            // Rotate the enemy to face the player
            FacePlayer(playerDirection);
            
            // If there is ground ahead of the enemy
            if (IsGroundAhead())
            {
                MoveTowardsPlayer(playerDirection);
            }
            // If there is no ground ahead, stop moving
            else
            {
                //stop moving if there is no ground ahead
                StopMoving();
            }
        }
        else
        {
            //stop moving if the player is not within the chase range
            StopMoving();
        }
        // if the player is not within the chase range, the above else statement will not run
        // because it only applies to the if statement directly above it
    }
    
    // bool function to check if there is ground in front of the enemy to walk on
    bool IsGroundAhead()
    {
        
        // Ground check variables
        float groundCheckDistance = 2.0f; // adjust this distance as needed
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        
        //determine which direction the enemy is facing
        Vector2 enemyFacingDirection = transform.rotation.y == 0 ? Vector2.left : Vector2.right;
        
        // Raycast to check for ground ahead of the enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down + enemyFacingDirection, groundCheckDistance, groundLayer);
        
        // Return true if ground is detected
        return hit.collider != null;
    }
    
    private void FacePlayer(Vector2 playerDirection)
    {
        //if the player is to the right of the eпету
        if (playerDirection.x < 0)
        {
            //face right
            sr.flipX = false;
        }
        //if the player is to the left of the eпету
        else if (playerDirection.x > 0)
        {
            //face left
            sr.flipX = true;
        }
    }
    
    private void MoveTowardsPlayer(Vector2 playerDirection)
    {
        // Move the enemy towards the player by setting the velocity
        // to a new Vector2 without changing the y axis of velocity
        rb.velocity = new Vector2(playerDirection.x * enemyMovementSpeed, rb.velocity.y);
        
        //set the animator parameter to move
        anim.SetBool("isMoving", true);
    }
    
    private void StopMoving()
    {
        // Stop moving if the player is out of range
        rb.velocity = new Vector2(0, rb.velocity.y);
    
        //set the animator parameter to stop moving
        anim.SetBool("isMoving", false);
    }
    
}
