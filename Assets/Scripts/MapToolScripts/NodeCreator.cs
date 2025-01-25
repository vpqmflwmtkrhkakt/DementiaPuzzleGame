using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCreator : MonoBehaviour
{
    [SerializeField]
    private ToolNode _nodePrefab;

    private NodePlacer _nodePlacer;

    private void Start()
    {
        Debug.Assert(_nodePrefab != null);

        _nodePlacer = GameObject.Find("NodePlacer").GetComponent<NodePlacer>();
        Debug.Assert(_nodePlacer != null);
    }

    public void CreateNode(Color nodeColor)
    {
        if(_nodePlacer.IsHoldingNode() == true)
        {
            return;
        }

        ToolNode node = Instantiate(_nodePrefab, Vector3.zero, Quaternion.identity, null).GetComponent<ToolNode>();
        Debugger.CheckInstanceIsNullAndQuit(node);


        node.SetColor(nodeColor);  
        _nodePlacer.StartPlaceNode(node);
    }
}
