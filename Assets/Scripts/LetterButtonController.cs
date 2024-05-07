using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterButtonController : MonoBehaviour
{
    public TextMeshPro letterText;
    private readonly string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

    void Start()
    {
        // When starting, a random letter from the alphabet must be set
        char randomChar = alphabet[Random.Range(0, alphabet.Length)];
        letterText.text = randomChar.ToString();
    }
}
