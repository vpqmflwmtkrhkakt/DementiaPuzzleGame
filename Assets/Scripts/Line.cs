using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private Node _lineStarterNode;

    private void Awake()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();

        Debugger.CheckInstanceIsNullAndQuit( _lineRenderer );
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

}
