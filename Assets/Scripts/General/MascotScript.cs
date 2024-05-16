using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascotScript : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    private AudioClip CorrectAudioClip
    {
        get
        {
            return Resources.Load<AudioClip>("Audio/Feedback/correct");
        }
    }

    private AudioClip IncorrectAudioClip
    {
        get
        {
            return Resources.Load<AudioClip>("Audio/Feedback/incorrect");
        }
    }

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        TriggerAnimation(MascotAnimationType.WAVING);
    }


    public void TriggerAnimation(MascotAnimationType animationType)
    {
        switch (animationType)
        {
            case MascotAnimationType.CORRECT:
                audioSource.clip = CorrectAudioClip;
                audioSource.PlayDelayed(0.2f);
                animator.SetTrigger("Correct");
                break;
            case MascotAnimationType.INCORRECT:
                audioSource.clip = IncorrectAudioClip;
                audioSource.PlayDelayed(0.2f);
                animator.SetTrigger("Incorrect");
                break;
            case MascotAnimationType.WAVING:
                animator.SetTrigger("Waving");
                break;
        }
    }
}
