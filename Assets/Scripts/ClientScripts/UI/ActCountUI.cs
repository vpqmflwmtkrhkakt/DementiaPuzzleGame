using System.Diagnostics;
using TMPro;

public class ActCountUI : BaseUI
{
    private TextMeshProUGUI _countText;
    private void Awake()
    {
        _countText = GetComponentInChildren<TextMeshProUGUI>();

        Debug.Assert(_countText != null, "text component is null");
    }

    protected override void Start()
    {
        GameManager.Instance.OnActionCountChanged += UpdateCounttext;
        base.Start();
    }

    private void UpdateCounttext(uint count)
    {
        _countText.text = "Act Count : " + count.ToString();
    }

}
