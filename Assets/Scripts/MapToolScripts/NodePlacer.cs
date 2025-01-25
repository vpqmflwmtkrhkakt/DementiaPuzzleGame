using UnityEngine;

public class NodePlacer : Singleton<NodePlacer>
{
    private ToolNode _holdingNode;
    private ToolPlate _focusingPlate;
    private int _placeNodeCount;

    public void StartPlaceNode(ToolNode newNode)
    {
        if(newNode == null)
        {
            return;
        }

        _holdingNode = newNode;
        _placeNodeCount = 2;
    }

    public void SetFocusingPlate(ToolPlate plate)
    {
        _focusingPlate = plate;
    }

    private void FixedUpdate()
    {
        if(_holdingNode != null)
        {
            if (_focusingPlate != null)
            {
                _holdingNode.transform.position = _focusingPlate.transform.position;
            }
            else
            {
                _holdingNode.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            }
        }
    }

    public void PlaceHoldingNode()
    {
        if (_holdingNode == null || _focusingPlate == null)
        {
            return;
        }

        if(_focusingPlate.IsPlateEmpty() == false)
        {
            return;
        }

        _focusingPlate.SetPlacedNode(_holdingNode);
        --_placeNodeCount;

        if(_placeNodeCount > 0)
        {
            _holdingNode = Instantiate(_holdingNode);
        }
        else
        {
            _holdingNode = null;
        }
    }
}
