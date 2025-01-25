using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPlate : MonoBehaviour
{
    private static NodePlacer _nodePlacer;
    private ToolNode _placedNode;
    private void Awake()
    {
        if (_nodePlacer == null)
        {
            _nodePlacer = GameObject.Find("NodePlacer").GetComponent<NodePlacer>();
            Debug.Assert(_nodePlacer != null);
        }
    }
    private void OnMouseEnter()
    {
        _nodePlacer.SetFocusingPlate(this);
    }

    private void OnMouseExit()
    {
        _nodePlacer.SetFocusingPlate(null);
    }

    private void OnMouseDown()
    {
        _nodePlacer.PlaceHoldingNode();
    }

    public bool IsPlateEmpty()
    {
        return _placedNode == null;
    }

    public void SetPlacedNode(ToolNode node)
    {
        if(node == null)
        {
            Debug.Log("No Node is Holding");
            return;
        }

        if(false == IsPlateEmpty())
        {
            Debug.Log("Plate's already Has a node");
            return;
        }

        _placedNode = node;
    }

    public void RemovePlacedNode()
    {
        if(_placedNode == null)
        {
            return;
        }

        Destroy(_placedNode.gameObject);
        _placedNode = null;
    }
}
