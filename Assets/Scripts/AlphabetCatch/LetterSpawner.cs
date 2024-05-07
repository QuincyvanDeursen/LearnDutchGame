using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public TextMeshProUGUI letter;
    public Transform canvas;
    public float timeBtwSpawn;
    public float startTimeBtwSpawn;
    private float[] spawnPositionsLetters = new float[] { -80f, 0f, 80f };
    public CSVCharacterLoader csvCharacterLoader;
    private List<char> characters;
    void Start()
    {
        if (csvCharacterLoader != null)
        {
            characters = csvCharacterLoader.GetCharacterList();
        }
    }
    private void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            //Random letter kiezen en in de text zetten
            int randIndexLetter = Random.Range(0, characters.Count);
            letter.text = characters[randIndexLetter].ToString();

            //Random spawn point kiezen en vector aanmaken
            int randIndexPos = Random.Range(0, spawnPositionsLetters.Length);
            Vector3 spawnPosition = new Vector3(spawnPositionsLetters[randIndexPos], 128, 0f);

            //Game object aanmaken en positie instellen
            TextMeshProUGUI spawnedLetter = Instantiate(letter, canvas);
            spawnedLetter.transform.localPosition = spawnPosition;

            timeBtwSpawn = startTimeBtwSpawn;
        } 
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
