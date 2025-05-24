using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//require the AudioSource component to be attached
//to the GameObject this script is attached to [RequireComponent(typeof(AudioSource))]

public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip soundToPlay;
    public float volume = 1f;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        audioSource.PlayOneShot(soundToPlay, volume);
    }
}
