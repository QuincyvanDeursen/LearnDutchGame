using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    private LetterSpawner _letterSpawner;
    // Start is called before the first frame update
    void Start()
    {
        _letterSpawner = FindObjectOfType<LetterSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick() {
        var prevLines = GameObject.FindGameObjectsWithTag("Line");

        foreach (var line in prevLines) {
            Destroy(line);
        }
        
        _letterSpawner.SpawnLetter();
    }
}
