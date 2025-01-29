using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Plate : MonoBehaviour
{
    public Line PlacedLine { get; set; }

    private void OnMouseEnter()
    {
        Debug.Log("Mouse entered");
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse exit");
    }
}
