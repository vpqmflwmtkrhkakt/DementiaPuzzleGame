using System.Runtime.CompilerServices;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Color _color;
    private SpriteRenderer _circleRenderer;
    public Line DrawingLine { get; private set; }
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

}
