using UnityEngine;

public class Node : MonoBehaviour
{
    private Color _color;
    private SpriteRenderer _circleRenderer;
    private bool _isDrawing = false;

    public bool IsConnected { get; private set; }

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
    public void SetConnected()
    {
        IsConnected = true;
    }

    public void SetDisconnected()
    {
        IsConnected = false;

        // 이거 추후 연결돼있던 라인 제거할때 써야할듯
        // 이렇게하면 연결됐던 노드 두개가 호출해서 두번 호출됨 -> 한번만 호출되도록 해야함
        // GameManager.Instance.SetDisconnected();
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
        if(true == IsConnected)
        {
            return;
        }

        LineCreator.Instance.StartDrawLine(transform.position, _circleRenderer.color, this);
        _isDrawing = true;
    }

    private void OnMouseUp()
    {
        if(false == _isDrawing)
        {
            return;
        }
        // 임시로 일단 upㄲㅏ지 여기서 
       LineCreator.Instance.StopDrawLine();
        _isDrawing = false;
    }

}
