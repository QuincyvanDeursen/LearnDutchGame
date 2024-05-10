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
        if (TracedPoints == 0)
        {
            Debug.Log("Number traced");
        }

        //Check if any of the tracing points has a collision with the line
        // if (TracingPoints.Any(tracingPoint => tracingPoint.GetComponent<TracingPoint>()))
        // {
        //     Debug.Log("Number traced");
        // }

    }
}
