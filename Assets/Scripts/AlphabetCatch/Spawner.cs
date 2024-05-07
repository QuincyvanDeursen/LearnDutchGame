using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject letter;
    public Transform canvas;
    public float timeBtwSpawn;
    public float startTimeBtwSpawn;
    private float[] spawnPositionsX = new float[] { -80f, 0f, 80f };


    private void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            //Random spawn point kiezen en vector aanmaken
            int randIndex = Random.Range(0, spawnPositionsX.Length);
            Vector3 spawnPosition = new Vector3(spawnPositionsX[randIndex], 128, 0f);

            //Game object aanmaken en positie instellen
            GameObject spawnedLetter = Instantiate(letter, canvas);
            spawnedLetter.transform.localPosition = spawnPosition;

            timeBtwSpawn = startTimeBtwSpawn;
        } 
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
