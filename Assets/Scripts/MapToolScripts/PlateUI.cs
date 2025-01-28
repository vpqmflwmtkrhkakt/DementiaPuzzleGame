using System;
using TMPro;
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
        Debugger.CheckInstanceIsNullAndQuit(_rowField);

        _colField = transform.Find("ColumnField").GetComponent<TMP_InputField>();
        Debugger.CheckInstanceIsNullAndQuit(_colField);

        _plateCreator = FindObjectOfType<PlateCreator>();
        Debugger.CheckInstanceIsNullAndQuit(_plateCreator);

        _createBtn = GetComponentInChildren<Button>();
        Debugger.CheckInstanceIsNullAndQuit(_createBtn);

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

        Debugger.CheckInstanceIsNullAndQuit(_plateCreator.gameObject);
        _plateCreator.CreatePlates(row, col);
    }
}
