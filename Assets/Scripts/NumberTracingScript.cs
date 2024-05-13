using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NumberTracingScript : MonoBehaviour
{
    public GameObject[] TracingPoints;
    public int TracedPoints;

    private Button _nextButton;
    // Start is called before the first frame update
    void Start()
    {
        TracedPoints = TracingPoints.Length;
        _nextButton = GameObject.Find("NextButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TracedPoints == 0)
        {
            _nextButton.interactable = true;
        }
    }
}
