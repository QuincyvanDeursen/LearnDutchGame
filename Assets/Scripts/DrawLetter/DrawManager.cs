using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;

    public bool canDraw = true;
    [SerializeField] private Line _linePrefab;

    public const float minDistance = 0.1f;

    private Line _currentLine;
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame

    public void SetDrawEnabled(bool value)
    {
        canDraw = value;
    }
    void Update()
    {
        if (!canDraw) return;
        if (float.IsInfinity(Input.mousePosition.x) || float.IsInfinity(Input.mousePosition.y)) return;

        Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);

        if (Input.GetMouseButton(0))
            _currentLine.SetPosition(mousePos);
    }
}
