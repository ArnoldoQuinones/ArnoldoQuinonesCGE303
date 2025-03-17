using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTriggerZone : MonoBehaviour
{
    //public AudioClip scoreSound;

    //private AudioSource scoreAudio;

    //private void Start()
    //{
    //    scoreAudio = GetComponent<AudioSource>();
    //}

    //Create a variable to keep track of whether the trigger zone is active
    bool active = true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the trigger zone is active and the player enters the trigger zone
        if (active && collision.gameObject.tag == "Player")
        {
            //Deactivate the trigger zone
            active = false;

            //Add 1 to the score when the player enters the trigger zone
            ScoreManager.score++;

            //play coin cound effect
            PlatformerPlayerController playerController = collision.gameObject.GetComponent<PlatformerPlayerController>();

            playerController.PlayCoinSound();

            //destroy this gameobject
            Destroy(gameObject);
        }
    }
}
