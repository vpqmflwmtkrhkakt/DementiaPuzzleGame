using UnityEngine;

public class LineCreator : Singleton<LineCreator>
{
    public Vector3 LineStartPos { get; private set; }

    [SerializeField]
    private Line _linePrefab;
    private Line _currentHoldingLine;
    private Vector3 _lastMousePos = Vector3.zero;
    private Vector3 _lastLineEndPos;

    public void AddPlateToMidlineList(Plate plate)
    {
        if(_currentHoldingLine == null)
        {
            return;
        }

        if(_currentHoldingLine.IsPlateBehindLine(plate.transform.position) == true)
        {
            if (plate.PlacedLine != null)
            {
                _currentHoldingLine.AddPassedPlacedLinePlateCount();
            }
            else
            {
                _currentHoldingLine.AddEnteredPlate(plate);
            }
        }
    }

    public void RemovePlateFromMidlineList(Plate plate)
    {
        if(_currentHoldingLine == null)
        {
            return;
        }
        // 현재 그리는 방향과 역방향이면 Remove
        // 역방향 판단은 내적으로
        if(_currentHoldingLine.IsPlateBehindLine(plate.transform.position) == true)
        {
            if (plate.PlacedLine != null)
            {
                _currentHoldingLine.MinusPassedPlacedLinePlateCount();
            }
            else
            {
                _currentHoldingLine.RemoveEnteredPlate(plate);
            }
        }
    }

    public void StartDrawLine(Vector3 lineStartPos, Color lineColor, Node lineStarterNode)
    {
        if(lineStarterNode == null)
        {
            Debug.LogError("lineStarterNode is null");
            return;
        }
        LineStartPos = lineStartPos;

        _currentHoldingLine = Instantiate(_linePrefab, lineStartPos, Quaternion.identity, transform).GetComponent<Line>();
        _currentHoldingLine.SetLineStarterNode(lineStarterNode);
        _currentHoldingLine.SetLineColor(lineColor);

        if(lineStarterNode.DrawingLine == null)
        {
            lineStarterNode.ConnectLine(_currentHoldingLine);
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
            // Fix : 추후 라인들은 메모리풀로 재사용할수있도록 ㄱㄱ
            Destroy(_currentHoldingLine.gameObject);
        }
        else
        {
            PlaceCurrentLine();
        }
        _currentHoldingLine = null;
    }
    private void PlaceCurrentLine()
    {
        // Line 실제 배치
        Plate focusedPlate = Plate.CurrentFocusedPlate;

        _currentHoldingLine.PlaceLineToMidPlates();
        focusedPlate.PlaceEndLine(_currentHoldingLine);
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
            _currentHoldingLine.SetLinePosition(LineStartPos, lineEndPos);
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

}
