using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Plate : MonoBehaviour
{
    public Line PuttedLine { get; set; }

    private TextMeshPro _debugText;
    private SpriteRenderer _plateSprite;

    private void Start()
    {
        if(_debugText == null)
        {
            _debugText = GetComponent<TextMeshPro>();

            Debugger.CheckInstanceIsNull(_debugText);
        }

        _plateSprite = GetComponentInChildren<SpriteRenderer>();
        Debugger.CheckInstanceIsNull(_plateSprite);
    }

    private void Update()
    {
        if (_debugText != null)
        {
            _debugText.text = string.Format("{0}, {1}", transform.position.x, transform.position.y);
        }
    }


}
