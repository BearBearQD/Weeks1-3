using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayKuruKuru : MonoBehaviour
{
    [Header("Music Settings")]
    public AudioClip musicClip; // The audio clip to play
    public bool loopMusic = true; // Whether to loop the music
    public float volume = 1f; // Volume of the music (0 to 1)

    private AudioSource audioSource; // AudioSource component

    void Start()
    {
        // Ensure the music clip is assigned
        if (musicClip == null)
        {
            Debug.LogError("Music Clip is not assigned!");
            return;
        }

        // Add an AudioSource component to the GameObject
        audioSource = gameObject.AddComponent<AudioSource>();

        // Configure the AudioSource
        audioSource.clip = musicClip;
        audioSource.loop = loopMusic;
        audioSource.volume = volume;

        // Play the music
        audioSource.Play();
    }
}
