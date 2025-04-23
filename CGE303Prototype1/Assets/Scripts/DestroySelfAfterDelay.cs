using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterDelay : MonoBehaviour
{
    //The delay before the game object is destroyed
    public float delay = 2f;

    void Start()
    {
      //Destroy the game object after 2 seconds
        Destroy(gameObject, delay);
    }
}