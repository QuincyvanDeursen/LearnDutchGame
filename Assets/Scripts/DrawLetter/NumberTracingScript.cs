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

    private GameObject Mascot;
    private MascotScript _mascotScript;

    private Button _nextButton;
    private Button _deleteButton;

    private bool playOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        TracedPoints = TracingPoints.Length;
        _nextButton = GameObject.Find("NextButton").GetComponent<Button>();
        _deleteButton = GameObject.Find("DeleteButton").GetComponent<Button>();
        Mascot = GameObject.Find("Mascot");
        _mascotScript = Mascot.GetComponent<MascotScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TracedPoints == 0)
        {
            _nextButton.interactable = true;
            _deleteButton.interactable = false;
            if (playOnce)
            {
                _mascotScript.TriggerAnimation(MascotAnimationType.CORRECT);
                playOnce = false;
            }
        }
    }
}
