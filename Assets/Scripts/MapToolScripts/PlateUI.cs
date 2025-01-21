using System;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
public class PlateUI : MonoBehaviour
{
    private TMP_InputField _rowField;
    private TMP_InputField _colField;
    private Button _createBtn;
    private PlateCreator _plateCreator;



    void Start()
    {
        _rowField = transform.Find("RowField").GetComponent<TMP_InputField>();
        Debugger.CheckInstanceIsNull(_rowField);

        _colField = transform.Find("ColumnField").GetComponent<TMP_InputField>();
        Debugger.CheckInstanceIsNull(_colField);

        _plateCreator = FindObjectOfType<PlateCreator>();
        Debugger.CheckInstanceIsNull(_plateCreator);

        _createBtn = GetComponentInChildren<Button>();
        Debugger.CheckInstanceIsNull(_createBtn);

        _createBtn.onClick.AddListener(CreatePlates);
    }

    private void CreatePlates()
    {
        Int32 row, col;
        if(false == Int32.TryParse(_rowField.text, out row) || false == Int32.TryParse(_colField.text, out col))
        {
            Debug.LogError("Input field error");
            return;
        }

        Debugger.CheckInstanceIsNull(_plateCreator.gameObject);
        _plateCreator.CreatePlates(row, col);
    }
}
