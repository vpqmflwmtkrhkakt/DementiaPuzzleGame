using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();

        Debugger.CheckInstanceIsNull( _lineRenderer );
    }

    public void SetLineColor(Color lineColor)
    {
        if (_lineRenderer != null)
        {
            _lineRenderer.startColor = lineColor;
            _lineRenderer.endColor = lineColor;
        }
    }

    public void SetLinePosition(Vector3 startPos, Vector3 endPos)
    {
        if(_lineRenderer != null)
        {
            _lineRenderer.SetPosition(0, startPos);
            _lineRenderer.SetPosition(1, endPos);
        }
    }

}
