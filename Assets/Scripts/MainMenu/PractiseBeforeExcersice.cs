using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PractiseBeforeExcersice : MonoBehaviour
{
    private bool hasPractisedBefore = false;

    public GameObject[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        int hasPlayed = PlayerPrefs.GetInt("hasPractisedBefore", 0);
        if (hasPlayed == 0) hasPractisedBefore = false;
        else hasPractisedBefore = true;

        if (!hasPractisedBefore) {
            foreach (GameObject button in buttons) {
                button.GetComponent<Button>().interactable = false;
                button.transform.GetChild(2).gameObject.SetActive(true);
            }
        } else {
            foreach (GameObject button in buttons) {
                button.GetComponent<Button>().interactable = true;
                button.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
}
