using UnityEngine;
using UnityEngine.UI;

public class RemoveLineUI : BaseUI
{
    private void Awake()
    {
        Button removeBtn = transform.GetComponentInChildren<Button>();
        
        Debug.Assert(removeBtn != null, "remove btn is null");

        removeBtn.onClick.AddListener(RemoveLastLine);
    }

    private void RemoveLastLine()
    {
        LineCreator.Instance.RemoveLastPlacedLine();
    }

}
