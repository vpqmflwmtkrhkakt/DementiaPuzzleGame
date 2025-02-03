using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Plate : MonoBehaviour
{
    public Node PlacedNode { get; set; }
    public Line PlacedLine { get; private set; }

    public static Plate CurrentFocusedPlate { get; private set; }

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
        // 1. ���
        // 2. ����� ������ �� �������ϰ��
        // -> ���� �׸��� �����Ѵ�.
        // Fix : PlacedNode, PlacedLine �� �� �ϳ��� �������̽��� ��ӹ޵��� ����ٸ� �����ϱ� �� ��������
        if (PlacedNode != null)
        {
            if(PlacedNode.DrawingLine != null)
            {
                return;
            }

            LineCreator.Instance.StartDrawLine(transform.position, PlacedNode.GetNodeColor(), PlacedNode);
        }
        else if (PlacedLine != null && PlacedLine.IsEndOfLine(transform.position) == true)
        {
            Node startNode = PlacedLine.GetLineStarterNode();
            LineCreator.Instance.StartDrawLine(transform.position, startNode.GetNodeColor(), startNode);
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
        PlacedLine = line;
        line.SetLineEndPosition(new Vector3(transform.position.x, transform.position.y, 0f));

        if(PlacedNode != null)
        {
            Debug.Assert(line.GetLineStarterNode().GetNodeColor() == PlacedNode.GetNodeColor(), "Node Color is Different");

            PlacedNode.ConnectLine(line);
            GameManager.Instance.MinusRemainingConnectionCount();
        }
    }
}
