using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class NodeUI : MonoBehaviour
{
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


        _nodeColorPopupUI.ActiveUI(this);
    }

    public void SetNodeColor(Color nodeColor)
    {
        _nodeColorBtn.image.color = nodeColor;
    }
    private void CreateNode()
    {
        _nodeCreator.CreateNode(_nodeColorBtn.image.color);
    }


}
