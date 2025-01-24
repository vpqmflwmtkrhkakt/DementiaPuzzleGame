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

    // FIX : �̰� nodeui�� �ʹ� ��������. ���̶� ���� �ǹ̴� ����������... �� �� �ִ� �������?
    public void SetNodeColor(Color nodeColor)
    {
        _nodeUI.SetNodeColor(nodeColor);
    }
}
