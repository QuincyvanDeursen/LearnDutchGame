using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OddOneOutGuesserController : MonoBehaviour
{
    // Mascot element
    public GameObject mascotObject;
    private MascotScript mascotScript;

    public GameObject victoryScreen;
    private CreateRandomButtons createRandomButtons;
    
    public TextMeshProUGUI rangeTextElement;

    private OnClickListener onClickListener;

    private readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string currentRange;
    private char currentOddLetter;

    private int RangeLength
    {
        get
        {
            return LevelDifficulty switch
            {
                LevelDifficulty.EASY => 7,
                LevelDifficulty.MODERATE => 9,
                LevelDifficulty.HARD => 11,
                _ => 7,// Default is easy
            };
        }
    }

    // Game properties
    public LevelDifficulty LevelDifficulty
    {
        get
        {
            return LevelDifficultyManager.instance?.GetSelectedDifficulty() ?? LevelDifficulty.EASY;
        }
    }


    private int RequiredScore
    {
        get
        {
            return LevelDifficulty switch
            {
                LevelDifficulty.EASY => 5,
                LevelDifficulty.MODERATE => 10,
                LevelDifficulty.HARD => 15,
                _ => 5,// Default is easy
            };
        }
    }
    private int currentScore;

    // Text elements
    public TMP_Text scoreTextElement;


    void Start()
    {
        mascotScript = mascotObject.GetComponent<MascotScript>();
        createRandomButtons = gameObject.AddComponent<CreateRandomButtons>();
        onClickListener = gameObject.AddComponent<OnClickListener>();

        scoreTextElement.text = $"0/{RequiredScore}";

        SetClickListeners();
        NextLetterRange();

        createRandomButtons.SetButtonListeners(onClickListener, currentOddLetter);
    }

    private void SetClickListeners() {
        //If correct button is pressed
      onClickListener.OnCorrectButtonPressed += () =>
        {
            currentScore++;
            scoreTextElement.text = $"{currentScore}/{RequiredScore}";
            if (currentScore >= RequiredScore)
            {
                mascotScript.TriggerAnimation(MascotAnimationType.VICTORY);
                victoryScreen.SetActive(true);
            }
            else
            {
                mascotScript.TriggerAnimation(MascotAnimationType.CORRECT);
                NextLetterRange();
                createRandomButtons.SetButtonListeners(onClickListener, currentOddLetter);
            }
        };

        //If incorrect button is pressed
       onClickListener.OnIncorrectButtonPressed += () =>
        {
            mascotScript.TriggerAnimation(MascotAnimationType.INCORRECT);
            scoreTextElement.text = $"{currentScore}/{RequiredScore}";
        };
    }

        public void NextLetterRange()
    {
        System.Random random = new();
        int startIndex = random.Next(0, alphabet.Length - RangeLength);
        string letterRange = alphabet.Substring(startIndex, RangeLength);
        int replaceIndex = random.Next(0, letterRange.Length);

        char oddLetter;
        do
        {
            oddLetter = alphabet[random.Next(0, alphabet.Length)];
        } while (letterRange.Contains(oddLetter));

        currentRange = letterRange.Remove(replaceIndex, 1).Insert(replaceIndex, oddLetter.ToString());

        rangeTextElement.text = currentRange;
        currentOddLetter = oddLetter;
        createRandomButtons.SetButtonLetters(alphabet,currentOddLetter);
    }

}
