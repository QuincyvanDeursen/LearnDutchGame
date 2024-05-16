using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfPlayerHasWon : MonoBehaviour
{

    public int score = 0;
    public int maxScore = 0;

    public GameObject victoryScreen;

    // Update is called once per frame

    public void CheckPlayerScore() {
        if (score >= maxScore) {
            victoryScreen.SetActive(true);
        }
    }
}
