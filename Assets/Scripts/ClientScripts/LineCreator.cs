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
        // ���� �׸��� ����� �������̸� Remove
        // ������ �Ǵ��� ��������
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
            // Fix : ���� ���ε��� �޸�Ǯ�� �����Ҽ��ֵ��� ����
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
        // Line ���� ��ġ
        Plate focusedPlate = Plate.CurrentFocusedPlate;

        _currentHoldingLine.PlaceLineToMidPlates();
        focusedPlate.PlaceEndLine(_currentHoldingLine);
    }

    private bool CheckIsLinePlaceable()
    {
        // ���� �������� plate�� ���õ� ���ǰ˻���� ����
        Plate focusedPlate = Plate.CurrentFocusedPlate;

        if(focusedPlate == null)
        {
            return false;
        }

        // plate�� ������ �ȵ� ����
        // 1. �̹� �ش� plate�� ������ ����ִ�
        // 2. plate�� ���� node�� ���� �ٸ���
        // 3. plate�� ���� line�� ���� �ٸ���

        // placed�� node�� �ִ� ���,
        // 1. ���� ��
        // 2. ������ ���� �������� üũ
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
