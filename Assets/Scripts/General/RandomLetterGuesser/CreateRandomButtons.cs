using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateRandomButtons : MonoBehaviour
{
    private List<GameObject> letterButtons;

    void Awake()
    {
        letterButtons = GameObject.FindGameObjectsWithTag("RangeGuesserButton").ToList();
    }



    public void SetButtonLetters(string alphabet, char currentOddLetter)
    {
        var tempRange = new List<char>(alphabet);
        tempRange.Remove(currentOddLetter);

        for (int i = 0; i < letterButtons.Count; i++)
        {
            var currentButton = letterButtons[i].GetComponentInChildren<TextMeshProUGUI>();

            // Get a unique random letterr from the temporary alphabet
            var randomAlphabetLetter = tempRange[Random.Range(0, tempRange.Count)];

            // Change the button's text to the random letter
            currentButton.text = randomAlphabetLetter.ToString();

            // Remove the letter from the alphabet so there is no duplicate answers
            tempRange.Remove(randomAlphabetLetter);

            // Set the onclick listener (correct check) to the button
        }

        // Make sure the correct answer is also an option
        var randomWrongButton = letterButtons[Random.Range(0, letterButtons.Count)].GetComponentInChildren<TextMeshProUGUI>();
        randomWrongButton.text = currentOddLetter.ToString();
    }

    public void SetButtonListeners(OnClickListener listener, char letterToFind) {
        foreach (var button in letterButtons)
        {
            SetButtonEventListener(button, listener, letterToFind);
        }
    }
    private void SetButtonEventListener(GameObject button, OnClickListener onClickListener, char letterToFind)
    {
        button.GetComponent<Button>().onClick.RemoveAllListeners();
        button.GetComponentInParent<Button>().onClick.AddListener(() => onClickListener.OnClick(button.GetComponentInChildren<TextMeshProUGUI>().text, letterToFind.ToString()));
    }
}
