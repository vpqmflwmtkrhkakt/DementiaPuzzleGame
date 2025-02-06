using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private List<BaseUI> _uiPrefabList;
    [SerializeField]
    private GameObject _uiCanvas;

    private Dictionary<string, BaseUI> _uiContainer;
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
        _uiContainer = new Dictionary<string, BaseUI>();
        foreach (BaseUI uiPrefab in _uiPrefabList)
        {
            BaseUI ui = Instantiate(uiPrefab, _uiCanvas.transform);

            _uiContainer.Add(uiPrefab.name, ui);
        }

        GameManager.Instance.OnGameCleared += GameCleared;
    }

    private void GameCleared()
    {
        PopupUI("GameClearUI");
    }
}
