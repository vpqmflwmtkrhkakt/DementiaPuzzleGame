using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField]
    private Color _color;
    private SpriteRenderer _circleRenderer;

    public bool IsConnected { get; private set; }

    private void Start()
    {
        _circleRenderer = GetComponentInChildren<SpriteRenderer>();
        Debugger.CheckInstanceIsNull(_circleRenderer);

        _circleRenderer.color = new Color(_color.r,  _color.g, _color.b, 1f);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
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

}
