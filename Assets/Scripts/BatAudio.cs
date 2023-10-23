using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAudio : MonoBehaviour
{
    public AudioClip[] flapAudioClips;
    public AudioClip[] squeakAudioClips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFlap()
    {
        audioSource.PlayOneShot(flapAudioClips[Random.Range(0, flapAudioClips.Length)]);
    }

    public void PlaySqueak()
    {
        audioSource.PlayOneShot(squeakAudioClips[Random.Range(0, squeakAudioClips.Length)]);
    }



}
