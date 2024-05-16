using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonClickListener : MonoBehaviour
{
    // Start is called before the first frame update

    private bool comparingButtons = false;
    private bool firstButton = true;

    private GameObject firstButtonObject;
    private string letterToCompare;
    private MascotScript mascotScript;
    public GameObject mascot;

    public CheckIfPlayerHasWon checkIfPlayerHasWon;
    void Start() {
        mascotScript = mascot.GetComponent<MascotScript>();
        checkIfPlayerHasWon = gameObject.GetComponent<CheckIfPlayerHasWon>();
        
        foreach(GameObject button in GameObject.FindGameObjectsWithTag("MemoryButton")) {
            button.GetComponent<Button>().onClick.AddListener(ButtonClicked);
            checkIfPlayerHasWon.maxScore++;
        }
    }
    public void ButtonClicked()
    {
        //Check if 2 buttons are already being compared
        if (comparingButtons) {
            return;
        }

        //Make the text visible of the clicked button
        EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().enabled = true;
        
     
        if (firstButton)
        {
            firstButton = false;
            firstButtonObject = EventSystem.current.currentSelectedGameObject;
            letterToCompare = firstButtonObject.GetComponentInChildren<TMP_Text>().text;
        } else
        {
            //If the same button is clicked twice
            if (firstButtonObject == EventSystem.current.currentSelectedGameObject)
            {
                return;
            }

            comparingButtons = true;

            //Start coroutine to check if the letters match (coroutine because time is needed to see the second letter)
            StartCoroutine(CheckMatch(EventSystem.current.currentSelectedGameObject));
        }
    }

        IEnumerator CheckMatch(GameObject secondButtonObject)
    {
        yield return new WaitForSeconds(1f);

        if (letterToCompare.ToLower() == secondButtonObject.GetComponentInChildren<TMP_Text>().text.ToLower())
        {
            mascotScript.TriggerAnimation(MascotAnimationType.CORRECT);
            
            firstButtonObject.GetComponent<Button>().interactable = false;
            secondButtonObject.GetComponent<Button>().interactable = false;
            checkIfPlayerHasWon.score += 2;
            checkIfPlayerHasWon.CheckPlayerScore();
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
