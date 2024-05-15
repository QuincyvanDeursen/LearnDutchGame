using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHasPlayedBefore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("hasPractisedBefore", 1);
    }
}
