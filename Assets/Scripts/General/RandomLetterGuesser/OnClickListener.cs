using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickListener : MonoBehaviour
{
    public delegate void CorrectButtonClicked();
    public delegate void IncorrectButtonClicked();
    public event CorrectButtonClicked OnCorrectButtonPressed;
    public event IncorrectButtonClicked OnIncorrectButtonPressed;
    public void OnClick(string answer, string letterToFind)
    {
        if (answer == letterToFind)
        {
            OnCorrectButtonPressed?.Invoke();
        } else {
            OnIncorrectButtonPressed?.Invoke();
        }
    }
}
