using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public List<GameObject> Letters;

    private GameObject _currentLetter;

    // Start is called before the first frame update
    void Start()
    {
        _currentLetter = SpawnLetter();
    }

    // Update is called once per frame
    void Update()
    {
        // if (_currentLetter != null || Letters.Count == 0) return;

        // SpawnLetter();
    }

    public GameObject SpawnLetter()
    {
        GameObject newLetter;

        //First run
        if (_currentLetter == null)
        {
            newLetter = Letters[0];
        }
        else
        {
            var index = Letters.FindIndex(x => x.name == _currentLetter?.name);

            if (index == -1 || index + 1 >= Letters.Count) return null;

            newLetter = Letters[index + 1];

            Destroy(_currentLetter);
        }

        _currentLetter = Instantiate(newLetter, transform.position, Quaternion.identity);
        _currentLetter.name = newLetter.name;

        return _currentLetter;
    }
}