using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MemoryStart : MonoBehaviour
{
    // Start is called before the first frame update

    private List<GameObject> buttons;
    private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private  int buttonCount;

    void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("MemoryButton").ToList();

       buttonCount = buttons.Count;
        for (int i = 0; i < buttonCount/2; i++) {
           //get Random button and assign random letter
            int randomButton = Random.Range(0, buttons.Count);
            int randomLetter = Random.Range(0, alphabet.Length);
                
                //Place text on button
            PlaceTextOnButton(buttons[randomButton], alphabet[randomLetter].ToString().ToUpper());
                //remove button from list
            buttons.RemoveAt(randomButton);
                //find a next button to assign matching letter
            int matchingButton = Random.Range(0, buttons.Count);

                //Place text on matching button
            PlaceTextOnButton(buttons[matchingButton], alphabet[randomLetter].ToString().ToLower());
                
                //Remove matching button from list
            buttons.RemoveAt(matchingButton);
                //remove letter from list
            alphabet = alphabet.Remove(randomLetter, 1);
        }
    }

    public void PlaceTextOnButton(GameObject button, string text) {
        button.GetComponentInChildren<TMP_Text>().text = text;
        button.GetComponentInChildren<TMP_Text>().enabled = false;
    }

    public void CheckIfGameIsOver() {
        buttonCount -= 2;
        if (buttonCount == 0) {
            //Game won
        }
    }
}
