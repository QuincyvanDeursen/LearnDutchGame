using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudibleLetterGuesserController : MonoBehaviour
{
    // Mascot element
    public GameObject mascotObject;
    private MascotScript mascotScript;

    // Victory Screen elements
    public GameObject victoryScreen;

    // Text elements
    public TextMeshProUGUI[] letterButtons;

    private OnClickListener onClickListener;
    private CreateRandomButtons createRandomButtons;
    public TMP_Text scoreTextElement;

    // Level properties
    private LevelDifficulty Difficulty
    {
        get
        {
            return LevelDifficultyManager.instance?.GetSelectedDifficulty() ?? LevelDifficulty.EASY;
        }
    }
    private int score;

    private int RequiredScore
    {
        get
        {
            return Difficulty switch
            {
                LevelDifficulty.EASY => 5,
                LevelDifficulty.MODERATE => 10,
                LevelDifficulty.HARD => 15,
                _ => 5,// Default is easy
            };
        }
    }
    private string Alphabet
    {
        get
        {
            return Difficulty switch
            {
                LevelDifficulty.EASY => "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                LevelDifficulty.HARD => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
                _ => "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            };
        }
    }

    // Current letter properties
    private string currentLetter;
    private AudioClip CurrentLetterAudioClip
    {
        get
        {
            return Resources.Load<AudioClip>($"Audio/Alphabet/{currentLetter.ToUpper()}");
        }
    }

    // Start is called on the initiation of the object, when the level is loaded
    void Start()
    {
        mascotScript = mascotObject.GetComponent<MascotScript>();
        onClickListener = gameObject.AddComponent<OnClickListener>();
        createRandomButtons = gameObject.AddComponent<CreateRandomButtons>();

        scoreTextElement.text = $"0/{RequiredScore}";

        WaitScript waitScript = gameObject.AddComponent<WaitScript>();
        waitScript.OnWaitCompleted += () =>
        {
           NextLetter();
           SetClickListeners();
           createRandomButtons.SetButtonListeners(onClickListener, currentLetter[0]);
        };

        waitScript.StartWait(0.5f);
    }

    // Set a next random letter as the current letter and load the resource
    public void NextLetter()
    {
        currentLetter = Alphabet[UnityEngine.Random.Range(0, Alphabet.Length)].ToString();
        createRandomButtons.SetButtonLetters(Alphabet, currentLetter[0]);
        PlayLetterAudio();
    }
    

    // Play the audio file of the current letter
    public void PlayLetterAudio()
    {
        if (CurrentLetterAudioClip != null)
        {
            AudioSource.PlayClipAtPoint(CurrentLetterAudioClip, new Vector3(0, 0, -8));
        }
        else
        {
            Debug.LogError("Failed to load audio clip for letter " + CurrentLetterAudioClip);
        }
    }
        private void SetClickListeners() {
        //If correct button is pressed
      onClickListener.OnCorrectButtonPressed += () =>
        {
            score++;
            scoreTextElement.text = $"{score}/{RequiredScore}";
            if (score >= RequiredScore)
            {
                mascotScript.TriggerAnimation(MascotAnimationType.VICTORY);
                victoryScreen.SetActive(true);
            }
            else
            {
                mascotScript.TriggerAnimation(MascotAnimationType.CORRECT);
                NextLetter();
                createRandomButtons.SetButtonListeners(onClickListener, currentLetter[0]);
            }
        };

        //If incorrect button is pressed
        onClickListener.OnIncorrectButtonPressed += () =>
        {
            mascotScript.TriggerAnimation(MascotAnimationType.INCORRECT);
        };
    }
}
