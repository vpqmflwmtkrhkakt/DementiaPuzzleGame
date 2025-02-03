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

    public bool IsOkToPlaceMidline() 
    {
        if(_passedPlacedLinePlateCount == 1)
        {
            if(Plate.CurrentFocusedPlate != null)
            {
                Line PlacedLine = Plate.CurrentFocusedPlate.PlacedLine;

                if(PlacedLine == null)
                {
                    return false;
                }

                if(PlacedLine.IsEndOfLine(Plate.CurrentFocusedPlate.transform.position) == false)
                {
                    return false;
                }

                if(PlacedLine.GetLineStarterNode().IsSameColor(_lineRenderer.startColor) == false)
                {
                    return false;
                }

                return true;
            }
        }


        return _passedPlacedLinePlateCount == 0; 
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

    public void SetLinePosition(Vector3 startPos, Vector3 endPos)
    {
        Debug.Assert(_lineRenderer != null);

        _lineRenderer.SetPosition(0, startPos);
        _lineRenderer.SetPosition(1, endPos);
    }

    public void SetLineEndPosition(Vector3 endPos)
    {
        _lineRenderer.SetPosition(1, endPos);
    }

    public bool IsEndOfLine(Vector2 position)
    {
        Vector2 lineEndPos = _lineRenderer.GetPosition(1);

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
        Vector2 drawingDirection = (_lineRenderer.GetPosition(0) - _lineRenderer.GetPosition(1)).normalized;

        Vector2 fromPlatePosition = (platePosition - _lineRenderer.GetPosition(1)).normalized;

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
        foreach (Plate midplate in _enteredPlateList)
        {
            midplate.ClearMidLine();
        }
    }
}
