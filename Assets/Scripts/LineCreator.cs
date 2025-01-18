using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : Singleton<LineCreator>
{
    public bool IsOkToDrawLine { get; private set; }

    public Vector3 LineStartPos { get; private set; }

    [SerializeField]
    private Line _linePrefab;

    private Line _currentHoldingLine;
    public void StartDrawLine(Vector3 lineStartPos, Color lineColor)
    {
        LineStartPos = lineStartPos;
        IsOkToDrawLine = true;

        _currentHoldingLine = Instantiate(_linePrefab, lineStartPos, Quaternion.identity, transform).GetComponent<Line>();
        _currentHoldingLine.SetLineColor(lineColor);
    }
    public void StopDrawLine()
    {
        LineStartPos = Vector3.zero;
        IsOkToDrawLine = false;

    }
    private void Update()
    {
        if(true == IsOkToDrawLine)
        {
            DrawLine();
        }
    }

    private void DrawLine()
    {
        Vector3 lineEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lineEndPos.z = 0f;

        if (_currentHoldingLine != null)
        {
            _currentHoldingLine.SetLinePosition(LineStartPos, lineEndPos);
        }
    }
}
