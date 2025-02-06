using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WarningMsgUI : BaseUI
{
    private TextMeshProUGUI _msg;
    private WaitForSeconds _waitSeconds = new WaitForSeconds(1.5f);
    private RectTransform _backgroundImageTransform;
    private void Awake()
    {
        _msg = GetComponentInChildren<TextMeshProUGUI>();

        Debugger.CheckInstanceIsNullAndQuit(_msg);

        _backgroundImageTransform = transform.Find("BackgroundImage").GetComponent<RectTransform>();

        Debugger.CheckInstanceIsNullAndQuit(_backgroundImageTransform);   
    }

    private IEnumerator CloseUIAfterSeconds()
    {
        yield return _waitSeconds;

        _msg.text = null;
        CloseUI();
    }
    public void ShowMessage(string msg)
    {
        StopAllCoroutines();
        _msg.text = msg;

        _backgroundImageTransform.sizeDelta = new Vector2(_msg.preferredWidth + 100f, _msg.preferredHeight + 100f);

        PopupUI();

        StartCoroutine(CloseUIAfterSeconds());
    }
}
