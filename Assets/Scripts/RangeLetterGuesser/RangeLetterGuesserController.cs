using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangeLetterGuesserController : MonoBehaviour
{
    // Mascot element
    public GameObject mascotObject;
    private MascotScript mascotScript;

    // Game properties
    public LevelDifficulty levelDifficulty;
    private int rangeLength
    {
        get
        {
            switch (levelDifficulty)
            {
                case LevelDifficulty.EASY:
                    return 7;
                case LevelDifficulty.MODERATE:
                    return 5;
                case LevelDifficulty.HARD:
                    return 3;
                default:
                    return 7; // Default is easy
            }
        }
    }

    private int requiredScore
    {
        get
        {
            switch (levelDifficulty)
            {
                case LevelDifficulty.EASY:
                    return 5;
                case LevelDifficulty.MODERATE:
                    return 10;
                case LevelDifficulty.HARD:
                    return 15;
                default:
                    return 5; // Default is easy
            }
        }
    }

    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string currentRange;
    private char currentLetter;
    private int currentScore;

    // Text elements
    public TextMeshProUGUI[] letterButtons;
    public TMP_Text scoreTextElement;
    public TMP_Text rangeTextElement;

    // Start is called before the first frame update
    void Start()
    {
        mascotScript = mascotObject.GetComponent<MascotScript>();
        scoreTextElement.text = $"0/{requiredScore}";

        NextLetterRange();
        SetButtonLetters();
    }

    public void NextLetterRange()
    {
        System.Random random = new System.Random();
        int startIndex = random.Next(0, alphabet.Length - rangeLength);
        string letterRange = alphabet.Substring(startIndex, rangeLength);
        int replaceIndex = random.Next(0, letterRange.Length);

        currentRange = letterRange.Remove(replaceIndex, 1).Insert(replaceIndex, "_");
        currentLetter = letterRange[replaceIndex];

        rangeTextElement.text = currentRange;
    }

    public void SetButtonLetters()
    {
        var tempAlphabet = new List<char>(alphabet);
        tempAlphabet.Remove(currentLetter);

        for (int i = 0; i < letterButtons.Length; i++)
        {
            var currentButton = letterButtons[i];

            // Get a unique random letterr from the temporary alphabet
            var randomAlphabetLetter = tempAlphabet[UnityEngine.Random.Range(0, tempAlphabet.Count)];

            // Change the button's text to the random letter
            currentButton.text = randomAlphabetLetter.ToString();

            // Remove the letter from the alphabet so there is no duplicate answers
            tempAlphabet.Remove(randomAlphabetLetter);

            // Set the onclick listener (correct check) to the button
            SetButtonEventListener(currentButton);
        }

        // Make sure the correct answer is also an option
        var randomWrongButton = letterButtons[UnityEngine.Random.Range(0, letterButtons.Length)];
        randomWrongButton.text = currentLetter.ToString();
        SetButtonEventListener(randomWrongButton);
    }

    // Set the button evenlistener of the method to the letterbuttononclick method
    private void SetButtonEventListener(TextMeshProUGUI textMeshProUGUI)
    {
        textMeshProUGUI.GetComponentInParent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();
        textMeshProUGUI.GetComponentInParent<UnityEngine.UI.Button>().onClick.AddListener(() => LetterButton_Click(textMeshProUGUI.text));
    }

    public void LetterButton_Click(string answer)
    {
        if (answer == currentLetter.ToString())
        {
            // Increase score
            currentScore++;
            scoreTextElement.text = $"{currentScore}/{requiredScore}";

            if (currentScore >= requiredScore)
            {
                // TODO: Show winner popup and disable game input
                mascotScript.TriggerAnimation(MascotAnimationType.WAVING);
            }
            else
            {
                // Move on to the next letter;
                NextLetterRange();
                SetButtonLetters();
            }

            // Play happy animation
            mascotScript.TriggerAnimation(MascotAnimationType.CORRECT);
        }
        else
        {
            // Play the incorrect animation on the mascot (wrong answer)
            mascotScript.TriggerAnimation(MascotAnimationType.INCORRECT);
        }
    }
}
