using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private List<UIBase> _uiPrefabList;
    [SerializeField]
    private GameObject _uiCanvas;

    private Dictionary<string, UIBase> _uiContainer;
    public void PopupUI(string uiName)
    {
        if(_uiContainer.ContainsKey(uiName) == false)
        {
            Debug.Log(uiName + "Ui doesn't exist");
            return;
        }

        _uiContainer[uiName].PopupUI();
    }

    private void Start()
    {
        _uiContainer = new Dictionary<string, UIBase>();
        foreach (UIBase uiPrefab in _uiPrefabList)
        {
            UIBase ui = Instantiate(uiPrefab, _uiCanvas.transform);

            _uiContainer.Add(uiPrefab.name, ui);
        }

        GameManager.Instance.OnGameCleared += GameCleared;
    }

    private void GameCleared()
    {
        PopupUI("GameClearUI");
    }
}
