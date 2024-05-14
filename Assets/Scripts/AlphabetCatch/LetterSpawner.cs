using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public TextMeshProUGUI letter;
    public Transform canvas;
    private float timeBtwSpawn;

    public int letterIndexTreshold = 1;
    public float startTimeBtwSpawn;
    private readonly float[] spawnPositionsLetters = new float[] { -600f, 0f, 600f };

    private readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    //Momentele letter waarvoor gezocht wordt
    private char current;

    //Om te voorkomen dat hetzelfde constant herhaalt wordt.
    private char lastDroppedLetter;
    private float lastLane = 0f;
    public TextMeshProUGUI lastLetterClicked;

    void Start()
    {
        current = alphabet[0];
    }

    public void HandleClick(char clickedLetter, GameObject letterObject)
    {
        print("Current: " + current);

        //Kijk of geklikte letter ook degene is waarnaar wordt gezocht.
        if (current != clickedLetter)
        {
            //Verkeerde letter
            
            //TODO
            return;

        }
        //Correcte letter dus update.
        lastLetterClicked.text = current.ToString();
        current = alphabet[alphabet.IndexOf(current) + 1];
        Destroy(letterObject);
    }

    private float GetRandomLaneSpawnPos()
    {
        float lane = spawnPositionsLetters[Random.Range(0, spawnPositionsLetters.Length)];
        //Als lane hetzelfde is als laatste lane, kies nieuwe lane
        if (lane == lastLane) return GetRandomLaneSpawnPos();
            lastLane = lane;
            return lane;
    }

    private char GetRandomLetterBasedOnCurrentLetter(int currentIndex)
    {
        //Willekeurige letter gebaseerd op huidige letter en treshold (C = A - E)
        char letter = alphabet[Random.Range(currentIndex - letterIndexTreshold, currentIndex + letterIndexTreshold)];
        //Als letter laatst gedropte letter is, kies nieuwe letter
        if (letter == lastDroppedLetter) return GetRandomLetterBasedOnCurrentLetter(currentIndex);
        lastDroppedLetter = letter;
        return letter;
    }

    void Update()
    {
        if (timeBtwSpawn > 0)
        {
            timeBtwSpawn -= Time.deltaTime;
            return;
        }

        //Momentele letter index vinden
        int currentIndex = alphabet.IndexOf(current);

        //Checken of treshold over limieten gaat <0 of >26
        if (currentIndex < letterIndexTreshold) currentIndex = letterIndexTreshold;
        else if (currentIndex > alphabet.Length - letterIndexTreshold) currentIndex = alphabet.Length - letterIndexTreshold;

        //Willekeurige letter kiezen die niet overeenkomt met de laatst gedropte letter en in de buurt zit van huidige letter
        letter.text = GetRandomLetterBasedOnCurrentLetter(currentIndex).ToString();

        //Willekeurige spawn point kiezen (anders dan laatste) en vector aanmaken
        Vector3 spawnPosition = new Vector3(GetRandomLaneSpawnPos(), 1500, 0f);

        //Game object aanmaken en positie instellen
        TextMeshProUGUI spawnedLetter = Instantiate(letter, canvas);
        spawnedLetter.transform.localPosition = spawnPosition;

        //Reset timeBtwSpawn
        timeBtwSpawn = startTimeBtwSpawn;
    }
}
