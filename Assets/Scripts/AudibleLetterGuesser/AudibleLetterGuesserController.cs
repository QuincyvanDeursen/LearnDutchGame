using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudibleLetterGuesserController : MonoBehaviour
{
    // Mascot element
    public GameObject mascotObject;
    private MascotScript mascotScript;

    // Text elements
    public TextMeshProUGUI[] letterButtons;
    public TMP_Text scoreTextElement;

    // Level properties
    public LevelDifficulty difficulty;
    private int score;
    private string alphabet
    {
        get
        {
            return difficulty switch
            {
                LevelDifficulty.EASY => "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
                LevelDifficulty.HARD => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ",
                _ => "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
            };
        }
    }

    // Current letter properties
    private string currentLetter;
    private AudioClip currentLetterAudioClip
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

        NextLetter();
        SetButtonLetters();
    }

    // Set a next random letter as the current letter and load the resource
    public void NextLetter()
    {
        currentLetter = alphabet[UnityEngine.Random.Range(0, alphabet.Length)].ToString();

        PlayLetterAudio();
    }

    // Play the audio file of the current letter
    public void PlayLetterAudio()
    {
        if (currentLetterAudioClip != null)
        {
            AudioSource.PlayClipAtPoint(currentLetterAudioClip, new Vector3(0, 0, -8));
        }
        else
        {
            Debug.LogError("Failed to load audio clip for letter " + currentLetterAudioClip);
        }
    }

    // Set new text in the buttons (the currentletter is always present)
    public void SetButtonLetters()
    {
        var tempAlphabet = new List<char>(alphabet);
        tempAlphabet.RemoveAll(c => char.ToLower(c) == char.ToLower(currentLetter[0]));

        for (int i = 0; i < letterButtons.Length; i++)
        {
            var currentLetterText = letterButtons[i];
            var randomAlphabetLetterIndex = UnityEngine.Random.Range(0, tempAlphabet.Count);
            var randomAlphabetText = tempAlphabet[randomAlphabetLetterIndex].ToString();

            // Set the text in the current text box
            currentLetterText.text = randomAlphabetText;

            // Remove the random letter from the temporary alphabet
            tempAlphabet.RemoveAll(c => char.ToLower(c) == char.ToLower(randomAlphabetText[0]));

            // Add click event listener to the button
            SetButtonEventListener(currentLetterText);
        }

        // Make sure the currentLetter is included in a random index
        var randomWrongElement = letterButtons[UnityEngine.Random.Range(0, letterButtons.Length)];
        randomWrongElement.text = currentLetter;
        SetButtonEventListener(randomWrongElement);
    }

    // Set the button evenlistener of the method to the letterbuttononclick method
    private void SetButtonEventListener(TextMeshProUGUI textMeshProUGUI)
    {
        textMeshProUGUI.GetComponentInParent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        textMeshProUGUI.GetComponentInParent<UnityEngine.UI.Button>().onClick.AddListener(() => LetterButtonOnClick(textMeshProUGUI.text));
    }

    public void LetterButtonOnClick(string chosenLetter)
    {
        if (chosenLetter == currentLetter)
        {
            // Play happy animation
            mascotScript.TriggerAnimation(MascotAnimationType.CORRECT);

            score++;
            scoreTextElement.text = $"{score}/10";

            if (score >= 10)
            {
                // TODO: Show winner popup and disable game input
                mascotScript.TriggerAnimation(MascotAnimationType.WAVING);
            }
            else
            {
                NextLetter();
                SetButtonLetters();
            }
        }
        else
        {
            // Play angry animation
            mascotScript.TriggerAnimation(MascotAnimationType.INCORRECT);
        }
    }
}
