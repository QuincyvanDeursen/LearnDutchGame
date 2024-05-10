using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private EdgeCollider2D _collider;

    private readonly List<Vector2> _points = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();   
        _collider.transform.position -= transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(Vector2 pos) {
        if (!CanAppend(pos)) return;

        _points.Add(pos);

        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pos);

        // if (_points.Count > 1) {
        //     _collider.points = _points.ToArray();
        // }
        _collider.points = _points.ToArray();
    }

    private bool CanAppend(Vector2 pos) {
        if (_lineRenderer.positionCount == 0) return true;

        return Vector2.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), pos) > DrawManager.minDistance;
    }
}
