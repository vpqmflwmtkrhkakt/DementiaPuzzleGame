using UnityEngine;
using TMPro;

public class Plate : MonoBehaviour
{
    public Node PlacedNode { get; set; }
    public Line PlacedLine { get; private set; }

    public static Plate CurrentFocusedPlate { get; private set; }

    [SerializeField]
    private TextMeshPro _debugText;

    private void Update()
    {
        if(PlacedLine == null)
        {
            _debugText.text = "null";
        }
        else
        {
            _debugText.text = "line";
        }
    }
    private void OnMouseEnter()
    {
        CurrentFocusedPlate = this;

        if(PlacedNode == null)
        {
            LineCreator.Instance.AddPlateToMidlineList(this);
        }
    }

    private void OnMouseExit()
    {
        CurrentFocusedPlate = null;

        if (PlacedLine == null && PlacedNode == null)
        {
            LineCreator.Instance.RemovePlateFromMidlineList(this);
        }
    }

    private void OnMouseDown()
    {
        // 1. 노드
        // 2. 연결된 라인의 맨 끝지점일경우
        // -> 선을 그리기 시작한다.
        if (PlacedNode != null)
        {
            if(PlacedNode.DrawingLine != null)
            {
                return;
            }

            LineCreator.Instance.StartDrawLine(transform.position, PlacedNode);
        }
        else if (PlacedLine != null && PlacedLine.IsEndOfLine(transform.position) == true)
        {
            Node startNode = PlacedLine.GetLineStarterNode();
            LineCreator.Instance.StartDrawLine(transform.position, startNode);
        }
    }

    private void OnMouseUp()
    {
        if(CurrentFocusedPlate == null)
        {
            return;
        }

        LineCreator.Instance.StopDrawLine();
    }

    public void PlaceMidLine(Line line)
    {
        Debug.Assert(line != null, "midline is null!"); 
        PlacedLine = line;
    }

    public void ClearMidLine()
    {
        PlacedLine = null;
    }

    public void PlaceEndLine(Line line)
    {
        Debug.Assert(line != null, "Placing line is null!");

        if(PlacedLine == null)
        {
            PlacedLine = line;
        }

        line.SetLineEndPosition(new Vector3(transform.position.x, transform.position.y, 0f));

        if(PlacedNode != null)
        {
            Debug.Assert(line.GetLineStarterNode().GetNodeColor() == PlacedNode.GetNodeColor(), "Node Color is Different");

            PlacedNode.ConnectLine(line);
            GameManager.Instance.MinusRemainingConnectionCount();
        }
    }
}
