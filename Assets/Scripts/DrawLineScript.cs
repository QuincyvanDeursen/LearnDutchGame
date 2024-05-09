using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 previousPosition;

    [SerializeField]
    private float minDistance = 0.1f;

    [SerializeField, Range(0.1f, 1.0f)]
    private float lineWidth = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        previousPosition = transform.position;
        lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;

            if (Vector3.Distance(previousPosition, currentPosition) > minDistance) {

                if (previousPosition == transform.position) {
                lineRenderer.SetPosition(0, currentPosition);
                } else {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPosition);
                }
                previousPosition = currentPosition;
            }
        }
    }
}
