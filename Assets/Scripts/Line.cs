using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(Vector2 pos) {
        if (!CanAppend(pos)) return;

        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pos);
    }

    private bool CanAppend(Vector2 pos) {
        if (_lineRenderer.positionCount == 0) return true;

        return Vector2.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), pos) > DrawManager.minDistance;
    }
}
