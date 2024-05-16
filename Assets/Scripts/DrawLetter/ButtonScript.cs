using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private Spawner _letterSpawner;
    private Button _nextButton;
    private Button _deleteButton;

    // Start is called before the first frame update
    void Start()
    {
        _letterSpawner = FindObjectOfType<Spawner>();
        _nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        _deleteButton = GameObject.Find("DeleteButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        RemoveAllLines();

        _deleteButton.interactable = true;
        _nextButton.interactable = false;

        _letterSpawner.SpawnLetter();
    }

    public void RemoveAllLines()
    {
        var prevLines = GameObject.FindGameObjectsWithTag("Line");

        foreach (var line in prevLines)
        {
            Destroy(line);
        }
    }
}
