using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangeOddOneOutGuesserController : MonoBehaviour
{
    // Mascot element
    public GameObject mascotObject;
    private MascotScript mascotScript;

    public GameObject victoryScreen;

    // Game properties
    public LevelDifficulty levelDifficulty
    {
        get
        {
            return LevelDifficultyManager.instance?.GetSelectedDifficulty() ?? LevelDifficulty.EASY;
        }
    }
    private int rangeLength
    {
        get
        {
            switch (levelDifficulty)
            {
                case LevelDifficulty.EASY:
                    return 7;
                case LevelDifficulty.MODERATE:
                    return 9;
                case LevelDifficulty.HARD:
                    return 11;
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
    private char currentOddLetter;
    private int currentScore;

    // Text elements
    public TextMeshProUGUI[] letterButtons;
    public TMP_Text scoreTextElement;
    public TMP_Text rangeTextElement;

    void Start()
    {
        mascotScript = mascotObject.GetComponent<MascotScript>();
        scoreTextElement.text = $"0/{requiredScore}";

        NextLetterRange();
        SetButtonLetters();
    }

    private void NextLetterRange()
    {
        System.Random random = new System.Random();
        int startIndex = random.Next(0, alphabet.Length - rangeLength);
        string letterRange = alphabet.Substring(startIndex, rangeLength);
        int replaceIndex = random.Next(0, letterRange.Length);

        char oddLetter;
        do
        {
            oddLetter = alphabet[random.Next(0, alphabet.Length)];
        } while (letterRange.Contains(oddLetter));

        currentRange = letterRange.Remove(replaceIndex, 1).Insert(replaceIndex, oddLetter.ToString());

        rangeTextElement.text = currentRange;
        currentOddLetter = oddLetter;
    }

    private void SetButtonLetters()
    {
        var tempRange = new List<char>(currentRange);
        tempRange.Remove(currentOddLetter);

        for (int i = 0; i < letterButtons.Length; i++)
        {
            var currentButton = letterButtons[i];

            // Get a unique random letterr from the temporary alphabet
            var randomAlphabetLetter = tempRange[UnityEngine.Random.Range(0, tempRange.Count)];

            // Change the button's text to the random letter
            currentButton.text = randomAlphabetLetter.ToString();

            // Remove the letter from the alphabet so there is no duplicate answers
            tempRange.Remove(randomAlphabetLetter);

            // Set the onclick listener (correct check) to the button
            SetButtonEventListener(currentButton);
        }

        // Make sure the correct answer is also an option
        var randomWrongButton = letterButtons[UnityEngine.Random.Range(0, letterButtons.Length)];
        randomWrongButton.text = currentOddLetter.ToString();
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
        if (answer == currentOddLetter.ToString())
        {
            // Increase score
            currentScore++;
            scoreTextElement.text = $"{currentScore}/{requiredScore}";

            if (currentScore >= requiredScore)
            {
                // TODO: Show winner popup and disable game input
                mascotScript.TriggerAnimation(MascotAnimationType.WAVING);
                victoryScreen.SetActive(true);
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
