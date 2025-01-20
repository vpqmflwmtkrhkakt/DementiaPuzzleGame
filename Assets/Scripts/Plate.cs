using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Plate : MonoBehaviour
{
    public Line PuttedLine { get; set; }
    private SpriteRenderer _plateSprite;

    private void Start()
    {
        _plateSprite = GetComponentInChildren<SpriteRenderer>();
        Debugger.CheckInstanceIsNull(_plateSprite);
    }
}
