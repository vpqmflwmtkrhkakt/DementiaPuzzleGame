using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Color _nodeColor;
    private Button _nodeColorBtn;
    private Button _nodeCreateBtn;
    private NodeCreator _nodeCreator;
    private void Start()
    {
        _nodeColorBtn = transform.Find("NodeColorBtn").GetComponent<Button>();
        Debugger.CheckInstanceIsNullAndQuit(_nodeColorBtn);

        _nodeColorBtn.onClick.AddListener(OpenNodeColorUI);

        _nodeCreateBtn = transform.Find("NodeCreateBtn").GetComponent<Button>();
        Debugger.CheckInstanceIsNullAndQuit(_nodeCreateBtn);

        _nodeCreateBtn.onClick.AddListener(CreateNode);

        _nodeCreator = FindObjectOfType<NodeCreator>();
        Debugger.CheckInstanceIsNullAndQuit(_nodeCreator);
    }

    private void OpenNodeColorUI()
    {
    }
    private void CreateNode()
    {
        Color nodeColor = _nodeColor;

        _nodeCreator.CreateNode(nodeColor);
    }


}
