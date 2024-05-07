using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MemoryStart : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> buttons;
    private bool firstButton = true;
    private GameObject firstButtonObject;

    private bool comparingButtons = false;
    private string letterToCompare = "";
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    void Start()
    {
        int buttonCount = buttons.Count/2;
        for (int i = 0; i < buttonCount; i++) {
           //get Random button and assign random letter
            int randomButton = Random.Range(0, buttons.Count);
            int randomLetter = Random.Range(0, alphabet.Length);
            buttons[randomButton].GetComponentInChildren<TMP_Text>().text = alphabet[randomLetter].ToString();
            AddListenerToButton(buttons[randomButton]);
            buttons[randomButton].GetComponentInChildren<TMP_Text>().enabled = false;
                //remove button from list
            buttons.RemoveAt(randomButton);
                //find a next button to assign matching letter
            int matchingButton = Random.Range(0, buttons.Count);
            buttons[matchingButton].GetComponentInChildren<TMP_Text>().text = alphabet[randomLetter].ToString().ToLower();
            AddListenerToButton(buttons[matchingButton]);
            buttons[matchingButton].GetComponentInChildren<TMP_Text>().enabled = false;
                //Remove matching button from list
            buttons.RemoveAt(matchingButton);
                //remove letter from list
            alphabet = alphabet.Remove(randomLetter, 1);
        }
    }

    void AddListenerToButton(GameObject button) {
        button.GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    public void ButtonClicked()
    {
        //Check if 2 buttons are already being compared
        if (comparingButtons) {
            return;
        }

        EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().enabled = true;

        if (firstButton)
        {
            firstButton = false;
            firstButtonObject = EventSystem.current.currentSelectedGameObject;
            letterToCompare = firstButtonObject.GetComponentInChildren<TMP_Text>().text;
        }
        else
        {
            if (firstButtonObject == EventSystem.current.currentSelectedGameObject)
            {
                return;
            }

            comparingButtons = true;

            StartCoroutine(CheckMatch(EventSystem.current.currentSelectedGameObject));
        }
    }

    public void CheckIfGameIsOver() {
        if (buttons.Count == 0) {
            Debug.Log("Game Over");
        }
    }

    IEnumerator CheckMatch(GameObject secondButtonObject)
    {
        yield return new WaitForSeconds(1f);

        if (letterToCompare.ToLower() == secondButtonObject.GetComponentInChildren<TMP_Text>().text.ToLower())
        {
            Destroy(firstButtonObject);
            Destroy(secondButtonObject);

            CheckIfGameIsOver();
        }
        else
        {
            firstButtonObject.GetComponentInChildren<TMP_Text>().enabled = false;
            secondButtonObject.GetComponentInChildren<TMP_Text>().enabled = false;
        }
        comparingButtons = false;
        firstButton = true;
    }
}
