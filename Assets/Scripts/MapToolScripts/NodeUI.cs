using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class NodeUI : MonoBehaviour
{
    private Color _nodeColor;
    private Button _nodeColorBtn;
    private Button _nodeCreateBtn;
    private NodeCreator _nodeCreator;
    private NodeColorUI _nodeColorPopupUI;
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


        _nodeColorPopupUI = transform.parent.Find("NodeColorUI").GetComponent<NodeColorUI>();

        Debugger.CheckInstanceIsNullAndQuit(_nodeColorPopupUI);
    }

    private void OpenNodeColorUI()
    {
        if(true == _nodeColorPopupUI.gameObject.activeSelf)
        {
            Debug.Log("already enabled");
            return;
        }

        _nodeColorPopupUI.gameObject.SetActive(true);
    }
    private void CreateNode()
    {
        Color nodeColor = _nodeColor;

        _nodeCreator.CreateNode(nodeColor);
    }


}
