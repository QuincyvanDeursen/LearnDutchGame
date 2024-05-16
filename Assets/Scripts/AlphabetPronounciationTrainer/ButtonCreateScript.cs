using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCreateScript : MonoBehaviour
{

    public GameObject buttonPrefab;
    public GameObject letterContent;
    public GameObject numberContent;
    public GameObject row;
    private GameObject currentRow;

    public int columnCount = 3;

    public string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 30, 40, 50, 60, 70, 80, 90, 100};

    public GameObject AudioManager;

    void Start()
    {
        CreateAlphabetButtons(alphabet, letterContent);
        CreateNumberButtons(numbers, numberContent);
    }

    //Create buttons
    void CreateAlphabetButtons(string alphabet, GameObject content)
    {
        int index = 0;
        for (int i = 0; i < alphabet.Length; i++)
        {
            if (index % columnCount == 0)
            {
                currentRow = Instantiate(row, content.transform);
            }
            GameObject button = AddButtonWithText(currentRow, alphabet[i].ToString());
            string letter = alphabet[i].ToString(); 
            button.GetComponentInChildren<TextMeshProUGUI>().text = letter + letter.ToLower();
            //Add audioclip to onclick event of button
            AddAudioToButton(button, "Audio/Alphabet/", letter);
            index++;
        }
    }
    private GameObject AddButtonWithText(GameObject Row, string text)
    {
        GameObject button = Instantiate(buttonPrefab, currentRow.transform);
        button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        button.transform.SetParent(Row.transform);

        return button;
    }
    private void AddAudioToButton(GameObject button, string resourceFolder, string fileName)
    {
        button.GetComponentInChildren<Button>().onClick.AddListener(() => AudioManager.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>(resourceFolder + fileName)));
    }

    void CreateNumberButtons(int[] numbers, GameObject content)
    {
        int index = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            if (index % columnCount == 0)
            {
                currentRow = Instantiate(row, content.transform);
            }
            GameObject button = AddButtonWithText(currentRow, numbers[i].ToString());
            AddAudioToButton(button, "Audio/Numbers/", numbers[i].ToString());
            index++;
        }
    }
}
