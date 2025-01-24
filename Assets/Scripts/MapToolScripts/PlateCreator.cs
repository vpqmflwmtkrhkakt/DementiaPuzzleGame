using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCreator : MonoBehaviour
{
    [SerializeField]
    private ToolPlate _platePrefab;
    private readonly Int32 _platesDistOffset = 1;
    public void CreatePlates(Int32 row, Int32 col)
    {
        RemovePlates();
        PlacePlates(row, col);
    }
    private void RemovePlates()
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    private void PlacePlates(Int32 row, Int32 col)
    {
        Vector3 platesInitPos = new Vector3((row - 1) * -0.5f, (col - 1) * -0.5f, 1f);

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                Vector3 platePosition = new Vector3(platesInitPos.x + _platesDistOffset * j, platesInitPos.y + _platesDistOffset * i, 1f);
                ToolPlate plate = Instantiate(_platePrefab, platePosition, Quaternion.identity, this.transform).GetComponent<ToolPlate>();
            }
        }
    }
}
