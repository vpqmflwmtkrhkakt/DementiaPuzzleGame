using System.Runtime.CompilerServices;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Color _color;
    private SpriteRenderer _circleRenderer;
    public Line DrawingLine { get; private set; }

    private Node _connectedNode = null;

    public bool IsConnected() { return _connectedNode != null; }
    public bool IsSameColor(Color compareColor)
    {
        return _color == compareColor;
    }

    public Color GetNodeColor() { return _color; }
    public void SetNodeColor(Color color) { _color = color; } 
    private void Start()
    {
        _circleRenderer = GetComponentInChildren<SpriteRenderer>();
        Debugger.CheckInstanceIsNullAndQuit(_circleRenderer);

        _circleRenderer.color = new Color(_color.r,  _color.g, _color.b, 1f);
    }
    public void ConnectLine(Line line)
    {
        DrawingLine = line;
    }

    public void DisconnectLine()
    {
        DrawingLine = null;
    }

    public Node GetConnectedNode()
    {
        return _connectedNode;
    }
    public void ConnectNode(Node oppositNode)
    {
        Debug.Assert(oppositNode != null, "opposit node is null");
        _connectedNode = oppositNode;
    }

    public void DisconnectNode()
    {
        Debug.Assert(_connectedNode != null, "connected node is null");

        _connectedNode = null;
    }
}
