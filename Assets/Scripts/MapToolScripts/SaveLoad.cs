using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    private Button _saveBtn;
    private Button _loadBtn;

    void Start()
    {
        _saveBtn = transform.Find("SaveBtn").GetComponent<Button>();
        Debugger.CheckInstanceIsNullAndQuit(_saveBtn);
        _saveBtn.onClick.AddListener(SaveMap);

        _loadBtn = transform.Find("LoadBtn").GetComponent<Button>();
        Debugger.CheckInstanceIsNullAndQuit(_loadBtn);
        _loadBtn.onClick.AddListener(LoadMap);


    }

    private void SaveMap()
    {
        DataManager.Instance.SaveCurrentMap("save");
    }

    private void LoadMap()
    {
        DataManager.Instance.LoadMap("save");
    }

    
}
