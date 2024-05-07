using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterAudioController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLetterSound(AudioClip letterSound)
    {
        audioSource.PlayOneShot(letterSound);
    }
}
