using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private Color _color;
    private SpriteRenderer _circleRenderer;

    public bool IsConnected { get; private set; }

    public bool IsSameColor(Color compareColor)
    {
        return _color == compareColor;
    }

    public Color GetNodeColor() { return _color; }

    private void Start()
    {
        _circleRenderer = GetComponentInChildren<SpriteRenderer>();
        Debugger.CheckInstanceIsNull(_circleRenderer);

        _circleRenderer.color = new Color(_color.r,  _color.g, _color.b, 1f);
    }
    public void SetConnected()
    {
        IsConnected = true;
        GameManager.Instance.SetConnected();
    }

    public void SetDisconnected()
    {
        IsConnected = false;
        GameManager.Instance.SetDisconnected();
    }

    private void OnMouseEnter()
    {
        LineCreator.Instance.SetLastEnteredNode(this);
    }

    private void OnMouseExit()
    {
        LineCreator.Instance.SetLastEnteredNode(null);
    }

    private void OnMouseDown()
    {
        LineCreator.Instance.StartDrawLine(transform.position, _circleRenderer.color, this);
    }

    private void OnMouseUp()
    {
        // 임시로 일단 upㄲㅏ지 여기서 
       LineCreator.Instance.StopDrawLine();
    }

}
