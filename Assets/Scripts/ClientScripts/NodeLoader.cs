using UnityEngine;

public class NodeLoader
{
    private Node _nodePrefab;
    public NodeLoader(Node nodePrefab)
    {
        Debugger.CheckInstanceIsNullAndQuit(nodePrefab);
        _nodePrefab = nodePrefab;
    }

    public void LoadNode(Vector3 nodePos, Color nodeColor)
    {
        Node node = UnityEngine.GameObject.Instantiate(_nodePrefab, nodePos, Quaternion.identity).GetComponent<Node>();

        node.SetNodeColor(nodeColor);
    }
}
