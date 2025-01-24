using UnityEngine;
using UnityEngine.UI;

public class NodeColorUI : MonoBehaviour
{
    private Button _exitBtn;
    private NodeUI _nodeUI;
    private void Start()
    {
        _exitBtn = transform.Find("ExitBtn").GetComponent<Button>();
        Debugger.CheckInstanceIsNullAndQuit(_exitBtn);

        _exitBtn.onClick.AddListener(CloseUI);
    }
    private void CloseUI()
    {
        this.gameObject.SetActive(false);
    }

    public void ActiveUI(NodeUI nodeUI)
    {
        if(nodeUI == null)
        {
            return;
        }
        if(_nodeUI == null)
        {
            _nodeUI = nodeUI;
        }

        this.gameObject.SetActive(true);
    }

    // FIX : 이거 nodeui랑 너무 강결합임. 툴이라 딱히 의미는 없긴하지만... 뗄 수 있는 방법없나?
    public void SetNodeColor(Color nodeColor)
    {
        _nodeUI.SetNodeColor(nodeColor);
    }
}
