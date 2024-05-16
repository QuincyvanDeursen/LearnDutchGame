using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitchBehaviour : MonoBehaviour
{
    
    public GameObject letterContent;
    public GameObject numberContent;
    public TextMeshProUGUI buttonText;


    public GameObject scrollView;

    private bool showingLetters = true;
    // Start is called before the first frame update

    void Start()
    {
        letterContent.SetActive(showingLetters);
        numberContent.SetActive(!showingLetters);
        buttonText.text = showingLetters ? "123" : "ABC";
    }
    public void Onclick()
    {
        showingLetters = !showingLetters;

        buttonText.text = showingLetters ? "123" : "ABC";
        //Adjust the scroll view to show correct content
        scrollView.GetComponent<ScrollRect>().content = showingLetters ? letterContent.GetComponent<RectTransform>() : numberContent.GetComponent<RectTransform>();

        //Set the correct content to be active
        letterContent.SetActive(showingLetters);
        numberContent.SetActive(!showingLetters);
    }
}



