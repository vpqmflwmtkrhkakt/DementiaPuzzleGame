using UnityEngine;

public class NodeLoader
{
    private Node _nodePrefab;
    public NodeLoader(Node nodePrefab)
    {
        Debugger.CheckInstanceIsNullAndQuit(nodePrefab);
        _nodePrefab = nodePrefab;
    }

    public void LoadNode(Plate plate, Color nodeColor)
    {
        Vector3 nodePos = plate.transform.position;
        nodePos.z = 0f;

        Node node = UnityEngine.GameObject.Instantiate(_nodePrefab, nodePos, Quaternion.identity).GetComponent<Node>();
        node.SetNodeColor(nodeColor);

        plate.PlacedNode = node;
    }
}
