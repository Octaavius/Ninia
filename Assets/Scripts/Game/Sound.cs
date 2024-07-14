using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource hitSound; // Assign in the inspector
    public AudioSource gameOverSound; // Assign in the inspector
    public AudioSource gamemusic; // Assign in the inspector
    public AudioSource sliceSound; // Assign in the inspector

    // Call this method to play the hit sound
    public void PlayHitSound()
    {
        hitSound.Play();
    }

    // Call this method to play the game over sound
    public void PlayGameOverSound()
    {
        gameOverSound.Play();
    }
    
    // Call this method to play the game music
    public void PlayGameMusic()
    {
        gamemusic.Play();
    }   

    public void PlaySliceSound()
    {
        sliceSound.Play();
    }
}
