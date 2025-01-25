using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.WSA;

public class NodePlacer : Singleton<NodePlacer>
{
    private ToolNode _holdingNode;
    private ToolPlate _focusingPlate;
    private int _placeNodeCount = 2;

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

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(_holdingNode != null)
            {
                ToolNode siblingNode = _holdingNode.SiblingNode;
                Destroy(_holdingNode.gameObject);

                _holdingNode = null;

                if(siblingNode != null)
                {
                    siblingNode.PlacedPlate.StartReplaceNode();
                    _placeNodeCount = 2;
                }
            }
        }
    }

    public bool ReplaceNode(ToolNode node)
    {
        if(_holdingNode != null)
        {
            return false;
        }

        _holdingNode = node;
        return true;
    }

    public bool IsHoldingNode()
    {
        return _holdingNode != null ? true : false;
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

        Debug.Assert(_placeNodeCount >= 0);

        if(_placeNodeCount > 0)
        {
            ToolNode secondNode = Instantiate(_holdingNode);

            secondNode.SiblingNode = _holdingNode;
            _holdingNode = secondNode;
        }
        else
        {
            _holdingNode = null;
        }
    }
}
