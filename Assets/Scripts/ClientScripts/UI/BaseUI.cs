using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField]
    protected bool _isClosedWhenStart;

    protected virtual void Start()
    {
        if(_isClosedWhenStart == true)
        {
            CloseUI();
        }
    }
    public void PopupUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
