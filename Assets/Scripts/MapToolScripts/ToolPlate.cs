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
        if (_placedNode != null)
        {
            // 배치됐던 노드 재배치
            if(_nodePlacer.ReplaceNode(_placedNode) == true)
            {
                _placedNode = null;
            }
        }
        else
        {
            _nodePlacer.PlaceHoldingNode();
        }
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
}
