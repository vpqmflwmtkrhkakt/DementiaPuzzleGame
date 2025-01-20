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
    private Node _lastEnteredNode;
    private Vector3 _lastMousePos = Vector3.zero;
    private Vector3 _lastLineEndPos;

    public void SetLastEnteredNode(Node node)
    {
        _lastEnteredNode = node;
    }

    public void StartDrawLine(Vector3 lineStartPos, Color lineColor, Node lineStarterNode)
    {
        if(lineStarterNode == null)
        {
            Debug.LogError("lineStarterNode is null");
            return;
        }
        LineStartPos = lineStartPos;
        IsOkToDrawLine = true;

        _currentHoldingLine = Instantiate(_linePrefab, lineStartPos, Quaternion.identity, transform).GetComponent<Line>();
        _currentHoldingLine.SetLineStarterNode(lineStarterNode);
        _currentHoldingLine.SetLineColor(lineColor);
    }
    public void StopDrawLine()
    {
        LineStartPos = Vector3.zero;
        IsOkToDrawLine = false;

        CheckIsLinePlaceable();
    }

    private void CheckIsLinePlaceable()
    {
        if(_lastEnteredNode == null)
        {
            if (_currentHoldingLine != null)
            {
                Destroy(_currentHoldingLine.gameObject);
            }

            return;
        }

        Node lineStartNode = _currentHoldingLine.GetLineStarterNode();


        if(lineStartNode == _lastEnteredNode)
        {
            Debug.Log("You are Matching The Same Node");
            Destroy(_currentHoldingLine.gameObject);
            return;
        }

        if(false == _lastEnteredNode.IsSameColor(_currentHoldingLine.GetLineStarterNode().GetNodeColor()))
        {
            Debug.Log("The Color Is Differenet");
            Destroy(_currentHoldingLine.gameObject);
            return;
        }


        // TODO : ���� ������ �����ϴ��� üũ


        // ��Ī ����
        Debug.Log("The Color Is Matched");
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
        Vector3 lineEndPos = CalculateLineEndPosition();

        if (_currentHoldingLine != null)
        {
            _currentHoldingLine.SetLinePosition(LineStartPos, lineEndPos);
        }
    }

    // Fix : �ϴ� ���콺 ���̵�, Ⱦ�̵� ������ �翡 ���� �����̰� �س��� but ���ڿ�������
    // ���� �ڿ������� Fix
    private Vector3 CalculateLineEndPosition()
    {
        Vector3 lineEndPos = Vector3.zero;
        Vector3 updatedMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

        Vector3 delta = updatedMousePos - _lastMousePos;
        float xDist = Mathf.Abs(delta.x);
        float yDist = Mathf.Abs(delta.y);

        if (xDist > yDist)
        {
            lineEndPos = new Vector3(updatedMousePos.x, LineStartPos.y, 0f);
        }
        else if(yDist > xDist)
        {
            lineEndPos = new Vector3(LineStartPos.x, updatedMousePos.y, 0f);
        }
        else
        {
            lineEndPos = _lastLineEndPos;
        }

        _lastMousePos = updatedMousePos;
        _lastLineEndPos = lineEndPos;

        return lineEndPos;
    }
}
