using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.WSA;

public class NodePlacer : Singleton<NodePlacer>
{
    private ToolNode _holdingNode;
    private ToolPlate _focusingPlate;
    private PlateCreator _plateCreator;
    public void SetHoldingNode(ToolNode newNode)
    {
        if(newNode == null)
        {
            return;
        }

        _holdingNode = newNode;
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
                Vector3 holdingNodePos = new Vector3(_focusingPlate.transform.position.x, _focusingPlate.transform.position.y, 0f);
                _holdingNode.transform.position = holdingNodePos;
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
            RemoveHoldingNode(_holdingNode);
        }
    }

    public void RemoveHoldingNode(ToolNode node)
    {
        if(node == null)
        {
            return;
        }

        ToolNode siblingNode = _holdingNode.SiblingNode;
        Destroy(_holdingNode.gameObject);

        _holdingNode = null;

        if (siblingNode != null)
        {
            siblingNode.PlacedPlate.StartReplaceNode();
        }
    }

    public void LoadNode(Vector2Int placeIndex, Vector2Int siblingNodeIndex, ToolNode placeNode)
    {
        if(_plateCreator == null)
        {
            _plateCreator = GameObject.Find("PlateCreator").GetComponent<PlateCreator>();
            Debugger.CheckInstanceIsNullAndQuit(_plateCreator);
        }

        ToolPlate plate = _plateCreator.GetPlateByIndex(placeIndex);
        plate.PlaceNode(placeNode);

        ToolPlate siblingNodePlate = _plateCreator.GetPlateByIndex(siblingNodeIndex);
        ToolNode siblingNode = siblingNodePlate.GetPlacedNode();

        if (siblingNode != null)
        {
            siblingNode.SiblingNode = placeNode;
            placeNode.SiblingNode = siblingNode;
        }
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

        _focusingPlate.PlaceNode(_holdingNode);

        if(_holdingNode.SiblingNode == null)
        {
            ToolNode secondNode = Instantiate(_holdingNode);

            secondNode.SetColor(_holdingNode.GetColor());
            _holdingNode.SiblingNode = secondNode;
            secondNode.SiblingNode = _holdingNode;
            _holdingNode = secondNode;
        }
        else
        {
            _holdingNode = null;
        }
    }
}
