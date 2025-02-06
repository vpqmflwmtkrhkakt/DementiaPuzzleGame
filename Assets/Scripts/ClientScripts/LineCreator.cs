using System.Collections.Generic;
using UnityEngine;

public class LineCreator : Singleton<LineCreator>
{
    public Vector3 LineStartPos { get; private set; }

    [SerializeField]
    private Line _linePrefab;
    private Line _currentHoldingLine;
    private Vector3 _lastMousePos = Vector3.zero;
    private Vector3 _lastLineEndPos;
    private LinkedList<Line> _placedLineChain = new LinkedList<Line>();

    public bool IsHoldingLine() { return _currentHoldingLine != null; }
    //private void Start()
    //{
    //    //GameManager.Instance.OnLevelChange += ResetLineCreator;
    //}
    private void ResetLineCreator()
    {
        _currentHoldingLine = null;
        _lastLineEndPos = Vector3.zero;
        _placedLineChain.Clear();
    }
    public void AddOrRemoveFromMidlineList(Plate plate)
    {
        if(_currentHoldingLine == null)
        {
            return;
        }

        if(plate.HasEntered == false)
        {
            return;
        }

        if (_currentHoldingLine.GetLastPlacedLinePos().Equals(plate.transform.position))
        {
            return;
        }


        bool isPlatePassed = _currentHoldingLine.IsPlateBehindLine(plate.transform.position);

        // entered 했었던 plate만 하도록??
        if (isPlatePassed == true)
        {
            // 플레이트를 넘어서 지나침
            if (plate.PlacedLine != null || plate.PlacedNode != null)
            {
                _currentHoldingLine.AddPassedPlacedLinePlateCount();
            }
            else
            {
                _currentHoldingLine.AddEnteredPlate(plate);
            }
        }
        else
        {
            // 플레이트를 넘어서 지나침
            if (plate.PlacedLine != null || plate.PlacedNode != null)
            {
                _currentHoldingLine.MinusPassedPlacedLinePlateCount();
            }
            else
            {
                _currentHoldingLine.RemoveEnteredPlate(plate);
            }
        }
    }
    public void StartDrawLine(Vector3 lineStartPos, Node lineStarterNode)
    {
        if(lineStarterNode == null)
        {
            Debug.LogError("lineStarterNode is null");
            return;
        }

        if(lineStarterNode.DrawingLine == null)
        {
            _currentHoldingLine = Instantiate(_linePrefab, lineStartPos, Quaternion.identity, transform).GetComponent<Line>();
            _currentHoldingLine.SetLineStarterNode(lineStarterNode);
            _currentHoldingLine.SetLineColor(lineStarterNode.GetNodeColor());
            _currentHoldingLine.SetLineInitPos(lineStartPos);
            lineStarterNode.ConnectLine(_currentHoldingLine);

            LineStartPos = lineStartPos;
        }
        else
        {
            _currentHoldingLine = lineStarterNode.DrawingLine;
            LineStartPos = _currentHoldingLine.GetLastLinePos();
            _currentHoldingLine.AddLineCount();
        }
    }
    public void StopDrawLine()
    {
        if(_currentHoldingLine == null)
        {
            return;
        }

        LineStartPos = Vector3.zero;

        if (CheckIsLinePlaceable() == false)
        {
            _currentHoldingLine.EraseLastLine();
        }
        else
        {
            PlaceHoldingLine();
            GameManager.Instance.IncreaseActCount();
        }

        _currentHoldingLine = null;
    }
    private void PlaceHoldingLine()
    {
        // Line 실제 배치 plate
        Plate focusedPlate = Plate.CurrentFocusedPlate;

        if (focusedPlate.PlacedNode == null && focusedPlate.PlacedLine == null)
        {
            _currentHoldingLine.AddEnteredPlate(focusedPlate);
        }

        focusedPlate.PlaceEndLine(_currentHoldingLine);

        _currentHoldingLine.PlaceLineToMidPlates();


        _placedLineChain.AddLast(_currentHoldingLine);
    }

    private bool CheckIsLinePlaceable()
    {
        // 현재 놓으려는 plate와 관련된 조건검사부터 실행
        Plate focusedPlate = Plate.CurrentFocusedPlate;

        if(focusedPlate == null)
        {
            return false;
        }

        // plate에 놓으면 안될 조건
        // 1. 이미 해당 plate에 라인이 깔려있다
        // 2. plate에 놓인 node와 색이 다르다
        // 3. plate에 놓인 line과 색이 다르다

        // placed된 node가 있는 경우,
        // 1. 색을 비교
        // 2. 동일한 노드와 비교중인지 체크
        if(_currentHoldingLine.IsOkToPlaceMidline() == false)
        {
            return false;
        }

        Node placedNode = focusedPlate.PlacedNode;

        if (placedNode == null)
        {
            Line placedLine = focusedPlate.PlacedLine;

            if(placedLine == null)
            {
                return true;
            }

            Node node = placedLine.GetLineStarterNode();

            if(node.IsConnected() == true)
            {
                return false;
            }

            if(placedLine.IsEndOfLine(focusedPlate.transform.position) == false)
            {
                return false;
            }

            

            return placedLine.GetLineStarterNode().IsSameColor(_currentHoldingLine.GetLineStarterNode().GetNodeColor());
        }
        else
        {
            Node lineStartNode = _currentHoldingLine.GetLineStarterNode();

            if (lineStartNode == placedNode)
            {
                return false;
            }

            return placedNode.IsSameColor(lineStartNode.GetNodeColor());
        }
    }

    private void Update()
    {
        if(_currentHoldingLine != null)
        {
            DrawLine();
        }
    }

    private void DrawLine()
    {
        Vector3 lineEndPos = CalculateLineEndPosition();

        if (_currentHoldingLine != null)
        {
            _currentHoldingLine.SetLineEndPosition(lineEndPos);
        }
    }

    // Fix : 일단 마우스 종이동, 횡이동 움직임 양에 따라 움직이게 해놓음 but 부자연스럽다
    // 추후 자연스럽게 Fix
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

    public void RemoveLastPlacedLine()
    {
        if(_placedLineChain.Count == 0)
        {
            return;
        }

        Line removeLine = _placedLineChain.Last.Value;

        Node connectedNode = removeLine.GetLineStarterNode();

        if (connectedNode.IsConnected())
        {
            Node oppositNode = connectedNode.GetConnectedNode();
            Debug.Assert(oppositNode != null, "opposit node is null");

            oppositNode.DisconnectNode();
            connectedNode.DisconnectNode();

            GameManager.Instance.PlusRemainingConnectionCount();
        }

        _placedLineChain.RemoveLast();
        removeLine.EraseLastLine();

        GameManager.Instance.IncreaseActCount();
    }
}
