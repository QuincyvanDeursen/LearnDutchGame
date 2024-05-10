using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Line _linePrefab;

    public const float minDistance = 0.1f;

    private Line _currentLine;
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (float.IsInfinity(Input.mousePosition.x)) return;
        
        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);

        if (Input.GetMouseButton(0))
            _currentLine.SetPosition(mousePos);
    }
}
