using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolNode : MonoBehaviour
{
    private Color _color;
    private SpriteRenderer _circleRenderer;

    public void SetColor(Color nodeColor)
    {
        _color = nodeColor;
        _circleRenderer.color = new Color(_color.r, _color.g, _color.b, 1f);
    }
    private void Awake()
    {
        _circleRenderer = GetComponentInChildren<SpriteRenderer>();
        Debugger.CheckInstanceIsNullAndQuit(_circleRenderer);
    }


}
