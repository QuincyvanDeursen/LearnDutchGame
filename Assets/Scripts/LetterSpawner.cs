using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    public List<GameObject> Letters;
    protected GameObject _currentLetter;
    // Start is called before the first frame update
    void Start()
    {
        _currentLetter = SpawnLetter();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentLetter != null || Letters.Count == 0) return;

        SpawnLetter();
    }

    public GameObject SpawnLetter() {
        if (_currentLetter != null) Destroy(_currentLetter);

        int rand = Random.Range(0, Letters.Count);

        _currentLetter =  Instantiate(Letters[rand], transform.position, Quaternion.identity);

        return _currentLetter;
    }
}
