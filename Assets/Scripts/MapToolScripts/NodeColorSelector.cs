using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodeColorSelector : MonoBehaviour, IPointerClickHandler
{
    private NodeColorUI _nodeColorUI;
    private RectTransform _rectTransform;
    private Image _colorImage;
    private Texture2D _colorTexture;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        Debugger.CheckInstanceIsNullAndQuit(_rectTransform);

        _nodeColorUI = transform.GetComponentInParent<NodeColorUI>();
        Debugger.CheckInstanceIsNullAndQuit(_nodeColorUI);

        _colorImage = GetComponent<Image>();
        Debugger.CheckInstanceIsNullAndQuit(_colorImage);

        _colorTexture = _colorImage.sprite.texture;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(false == IsUserClickedInsideCircle(eventData.position))
        {
            return;
        }

        Vector2 localCursor;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _rectTransform,
            eventData.position,
            null,
            out localCursor
        );

        
        // UV��ǥ ȹ��
        Vector2 uvCoord = new Vector2(
          (localCursor.x + _rectTransform.rect.width / 2) / _rectTransform.rect.width,
          (localCursor.y + _rectTransform.rect.height / 2) / _rectTransform.rect.height
      );

        // UV��ǥ�� �ش��ϴ� �ȼ� ��ǥ ȹ��
        int x = Mathf.FloorToInt(uvCoord.x * _colorTexture.width);
        int y = Mathf.FloorToInt(uvCoord.y * _colorTexture.height);

        // Ŭ���� ��ġ�� �ȼ� ���� ��������
        Color selectedColor = _colorTexture.GetPixel(x, y);

        if(selectedColor.a < 1)
        {
            return;
        }

        // Creater UI�� �ѱ��
        _nodeColorUI.SetNodeColor(selectedColor);
    }

    private bool IsUserClickedInsideCircle(Vector2 mousePos)
    {
        float radius = _rectTransform.rect.width * 0.5f;

        float dist = Vector2.Distance(mousePos, _rectTransform.position);

        return dist < radius ? true : false;  
    }
}
