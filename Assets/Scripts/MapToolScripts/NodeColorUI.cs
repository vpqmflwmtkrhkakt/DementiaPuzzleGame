using UnityEngine;
using UnityEngine.UI;

public class NodeColorUI : MonoBehaviour
{
    private Button _exitBtn;

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
}
