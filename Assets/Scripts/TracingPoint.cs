using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingPoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Tracing point triggered");
        if (other.CompareTag("Line"))
        {
            Debug.Log("Tracing point triggered");
        }
    }
}
