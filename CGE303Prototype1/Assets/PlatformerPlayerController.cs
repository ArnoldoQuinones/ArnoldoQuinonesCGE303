using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayerController : MonoBehaviour
{
    //Player movement speed
    public float moveSpeed = 5f;

    //Variables for jumping
    public float jumpForce = 10f;       //Force applied when jumping
    public LayerMask groundLayer;       //Layer mask for detecting ground
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    // bool to keep track of if we are on the ground
    private bool isGrounded;


    //Reference to the Rigidbody2d component
    private Rigidbody2D rb;

    private float horizontalInput;

    public AudioClip jumpSound;
    public AudioClip coinSound;
//    public AudioClip scoreSound;
    private AudioSource playerAudio;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //Get the Rigidbody2D component attached to the GameObject
        rb = GetComponent<Rigidbody2D>();

        // Set the reference for the AudioSource
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        //ensure the groundCheck variable is assigned
        if (groundCheck == null)
        {
            Debug.LogError("groundCheck not assigned to the player controller!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get input for horizantal movement
        horizontalInput = Input.GetAxis("Horizontal");

        //Check for jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Apply an upward force for jumping
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Play jump sound effect
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }

    }

    void FixedUpdate()
    {
        //Move the player using Rigidbody2D in FixedUpdate
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        //set animator parameter xVelocityAbs to absolute value of x velocity
        animator.SetFloat("xVelocityAbs", Mathf.Abs(rb.velocity.x));

        //set animator parameter yVelocity to y velocity
        animator.SetFloat("yVelocity", rb.velocity.y);

        //Check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //set animator parameter onGround to isGrounded
        animator.SetBool ("onGround", isGrounded);


        //Ensure the player is facing the direction of movement
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Facing right
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Facing left
        }

    }

    public void PlayCoinSound()
    {
        //play coin sound
        playerAudio.PlayOneShot(coinSound, 1.0f);

    }


}
//    private AudioClip scoreAudio;

    

    


    //Set these references in the Inspector
//    public AudioClip jumpSound;