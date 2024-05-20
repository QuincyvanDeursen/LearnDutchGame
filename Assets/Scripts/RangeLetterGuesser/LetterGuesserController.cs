using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterGuesserController : MonoBehaviour
{
    // Mascot element
    public GameObject mascotObject;
    private MascotScript mascotScript;

    public GameObject victoryScreen;

    private CreateRandomButtons createRandomButtons;
    private OnClickListener onClickListener;

    // Game properties
    public LevelDifficulty LevelDifficulty
    {
        get
        {
            return LevelDifficultyManager.instance?.GetSelectedDifficulty() ?? LevelDifficulty.EASY;
        }
    }
    private int RangeLength
    {
        get
        {
            return LevelDifficulty switch
            {
                LevelDifficulty.EASY => 7,
                LevelDifficulty.MODERATE => 5,
                LevelDifficulty.HARD => 3,
                _ => 7,// Default is easy
            };
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

    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private string currentRange;
    private char currentLetter;
    private int currentScore;

    // Text element
    public TMP_Text scoreTextElement;
    public TMP_Text rangeTextElement;

    // Start is called before the first frame update
    void Start()
    {
        mascotScript = mascotObject.GetComponent<MascotScript>();
        createRandomButtons = gameObject.AddComponent<CreateRandomButtons>();
        onClickListener = gameObject.AddComponent<OnClickListener>();

        scoreTextElement.text = $"0/{RequiredScore}";

        SetClickListeners();
        char current = NextLetterRange();

        createRandomButtons.SetButtonListeners(onClickListener, current);
    }

    public char NextLetterRange()
    {
        System.Random random = new System.Random();
        int startIndex = random.Next(0, alphabet.Length - RangeLength);
        string letterRange = alphabet.Substring(startIndex, RangeLength);
        int replaceIndex = random.Next(0, letterRange.Length);

        currentRange = letterRange.Remove(replaceIndex, 1).Insert(replaceIndex, "_");
        currentLetter = letterRange[replaceIndex];

        rangeTextElement.text = currentRange;
        createRandomButtons.SetButtonLetters(alphabet, currentLetter);
        return currentLetter;
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
                char current = NextLetterRange();
                createRandomButtons.SetButtonListeners(onClickListener, current);
            }
        };

        //If incorrect button is pressed
        onClickListener.OnIncorrectButtonPressed += () =>
        {
            mascotScript.TriggerAnimation(MascotAnimationType.INCORRECT);
        };
    }
}
