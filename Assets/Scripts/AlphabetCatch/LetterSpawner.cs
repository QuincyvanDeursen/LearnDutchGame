using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class LetterSpawner : MonoBehaviour
{
    public TextMeshProUGUI letter;
    public Transform canvas;

    public GameObject mascot;
    private MascotScript mascotScript;

    public VideoClip videoClip;

    public GameObject victoryScreen;

    public float spawnHeight; 
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

    private bool countDownOver = false;
    public TextMeshProUGUI lettersToClick;
    public TextMeshProUGUI lettersClicked;

    private GameObject AudioManager;
    private int clickedIndex = 0;

    void Start()
    {
        WaitScript waitScript = gameObject.AddComponent<WaitScript>();
        waitScript.StartWait(3f);
        waitScript.OnWaitCompleted += () => {
            countDownOver = true;
        };

        AudioManager = GameObject.Find("AudioManager");

        current = alphabet[0];
        mascotScript = mascot.GetComponent<MascotScript>();
    }

    public void HandleClick(char clickedLetter, GameObject letterObject)
    {

        //Kijk of geklikte letter ook degene is waarnaar wordt gezocht.
        if (current != clickedLetter)
        {
            //Verkeerde letter
            
            //TODO
            mascotScript.TriggerAnimation(MascotAnimationType.INCORRECT);
            return;

        }
        //Correcte letter dus update.
        if (clickedLetter == 'Z') {
            //Win Game
            lettersClicked.text += clickedLetter;
            victoryScreen.SetActive(true);
            return;
        }
        current = alphabet[alphabet.IndexOf(current) + 1];
        lettersClicked.text += clickedLetter;
        clickedIndex++;

        AudioManager.GetComponent<AudioSource>().PlayOneShot(Resources.Load<AudioClip>("Audio/Alphabet/" + clickedLetter));
        
        if (clickedIndex % 3 == 0) {
            if (alphabet.IndexOf(current) + 2 >= alphabet.Length) {
                lettersToClick.text = current.ToString() + alphabet[alphabet.IndexOf(current) + 1];
            } else {
                lettersToClick.text = current.ToString() + alphabet[alphabet.IndexOf(current) + 1] + alphabet[alphabet.IndexOf(current) + 2];
            }
            lettersClicked.text = "";
        }
        mascotScript.TriggerAnimation(MascotAnimationType.CORRECT);
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
        if (!countDownOver) return;
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
        Vector3 spawnPosition = new Vector3(GetRandomLaneSpawnPos(), spawnHeight, 0f);

        //Game object aanmaken en positie instellen
        TextMeshProUGUI spawnedLetter = Instantiate(letter, canvas);
        spawnedLetter.transform.localPosition = spawnPosition;

        //Reset timeBtwSpawn
        timeBtwSpawn = startTimeBtwSpawn;
    }
}
