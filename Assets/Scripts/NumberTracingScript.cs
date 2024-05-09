using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NumberTracingScript : MonoBehaviour
{
    public GameObject[] TracingPoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if any of the tracing points has a collision with the line
        // if (TracingPoints.Any(tracingPoint => tracingPoint.GetComponent<TracingPoint>()))
        // {
        //     Debug.Log("Number traced");
        // }

    }
}
