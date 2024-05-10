using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NumberTracingScript : MonoBehaviour
{
    public GameObject[] TracingPoints;
    public int TracedPoints;
    // Start is called before the first frame update
    void Start()
    {
        TracedPoints = TracingPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        //Animate that letter is correctly written
        if (TracedPoints == 0)
        {
            Debug.Log("Number traced");
        }
    }
}
