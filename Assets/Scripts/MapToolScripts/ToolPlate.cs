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

    private void OnDestroy()
    {
        if(_placedNode != null)
        {
            Destroy(_placedNode.gameObject);
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
            StartReplaceNode();
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

    public void PlaceNode(ToolNode node)
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
        _placedNode.PlacedPlate = this;
        _placedNode.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    public ToolNode GetPlacedNode()
    {
        return _placedNode;
    }

    public void StartReplaceNode()
    {
        if (_placedNode != null)
        {
            if(_nodePlacer.IsHoldingNode() == false)
            {
                _nodePlacer.SetHoldingNode(_placedNode);

                _placedNode.PlacedPlate = null;
                _placedNode = null;
            }
        }
    }
}
