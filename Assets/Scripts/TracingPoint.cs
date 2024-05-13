using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingPoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Line"))
        {
            transform.parent.GetComponent<NumberTracingScript>().TracedPoints--;

            Destroy(gameObject);
        }
    }
}
