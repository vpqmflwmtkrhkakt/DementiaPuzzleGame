using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Node _lineStarterNode;
    private LinkedList<Plate> _enteredPlateList;
    private uint _passedPlacedLinePlateCount = 0;

    private void Awake()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();

        Debugger.CheckInstanceIsNullAndQuit( _lineRenderer );

        _enteredPlateList = new LinkedList<Plate>();
    }
    public void AddPassedPlacedLinePlateCount() 
    {
        _passedPlacedLinePlateCount++; 
    }
    public void MinusPassedPlacedLinePlateCount() 
    { 
        _passedPlacedLinePlateCount--; 
        Debug.Assert(_passedPlacedLinePlateCount >= 0, "passedPlateCount underflowed!"); 
    }

    public void AddLineCount()
    {
        _lineRenderer.positionCount++;
    }

    public void EraseLastLine()
    {
        _lineRenderer.positionCount--;

        if(_lineRenderer.positionCount <= 1)
        {
            Destroy(this.gameObject);
        }
    }

    public Vector2 GetLastLinePos()
    {
        return _lineRenderer.GetPosition( _lineRenderer.positionCount - 1);
    }

    public Vector2 GetLastPlacedLinePos()
    {
        return _lineRenderer.GetPosition(_lineRenderer.positionCount - 2);
    }

    public bool IsOkToPlaceMidline() 
    {
        if(_passedPlacedLinePlateCount > 0)
        {
            return false;
        }

        if (Plate.CurrentFocusedPlate != null)
        {
            Line PlacedLine = Plate.CurrentFocusedPlate.PlacedLine;

            if (PlacedLine == null)
            {
                return true;
            }

            if (PlacedLine.IsEndOfLine(Plate.CurrentFocusedPlate.transform.position) == false ||
                PlacedLine.GetLineStarterNode().IsSameColor(_lineRenderer.startColor) == false)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public Node GetLineStarterNode() 
    { 
        return _lineStarterNode; 
    }

    public void SetLineStarterNode(Node starterNode)
    {
        _lineStarterNode = starterNode;
    }

    public void SetLineColor(Color lineColor)
    {
        Debug.Assert(_lineRenderer != null);
        _lineRenderer.startColor = lineColor;
        _lineRenderer.endColor = lineColor;
    }

    public void SetLineInitPos(Vector3 startPos)
    {
        _lineRenderer.SetPosition(0, startPos);
    }
    public void SetLineEndPosition(Vector3 endPos)
    {
        Debug.Assert(_lineRenderer != null);

        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, endPos);
    }

    public bool IsEndOfLine(Vector2 position)
    {
        Vector2 lineEndPos = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);

        return lineEndPos == position;
    }

    public void AddEnteredPlate(Plate plate)
    {
        Debug.Assert(plate != null);

        _enteredPlateList.AddLast(plate);
    }

    public void RemoveEnteredPlate(Plate plate)
    {
        Debug.Assert(plate != null);

        _enteredPlateList.Remove(plate);
    }

    public bool IsPlateBehindLine(Vector3 platePosition)
    {
        Vector2 drawingDirection = (_lineRenderer.GetPosition(_lineRenderer.positionCount - 1) - _lineRenderer.GetPosition(_lineRenderer.positionCount - 2)).normalized;

        Vector2 fromPlatePosition = (platePosition - _lineRenderer.GetPosition(_lineRenderer.positionCount - 1)).normalized;

        return Vector2.Dot(fromPlatePosition, drawingDirection) < 0;
    }

    public void PlaceLineToMidPlates()
    {
        foreach (Plate midPlate in _enteredPlateList)
        {
            midPlate.PlaceMidLine(this);
        }
    }

    private void OnDisable()
    {
        _lineStarterNode = null;
        _passedPlacedLinePlateCount = 0;

        foreach (Plate midplate in _enteredPlateList)
        {
            midplate.ClearMidLine();
        }

        _enteredPlateList.Clear();
    }
}
