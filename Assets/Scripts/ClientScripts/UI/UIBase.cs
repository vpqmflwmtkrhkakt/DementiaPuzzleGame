using UnityEngine;

public class UIBase : MonoBehaviour
{
    public void PopupUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}
