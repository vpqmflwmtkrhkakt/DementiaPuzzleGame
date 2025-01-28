using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCreator : MonoBehaviour
{
    [SerializeField]
    private ToolPlate _platePrefab;
    private readonly Int32 _platesDistOffset = 1;
    public Vector2Int PlatesNum { get; private set; }
    private List<List<ToolPlate>> _plateList;
    public void CreatePlates(Int32 row, Int32 col)
    {
        RemovePlates();
        PlacePlates(row, col);
        PlatesNum = new Vector2Int(row, col);
    }
    private void RemovePlates()
    {
        int childCount = transform.childCount;

        if (_plateList != null)
        {
            foreach (List<ToolPlate> subList in _plateList)
            {
                foreach(ToolPlate plate in subList)
                {
                    Destroy(plate.gameObject); 
                }
                subList.Clear();
            }

            _plateList.Clear();
            _plateList = null;
        }
    }

    private void PlacePlates(Int32 row, Int32 col)
    {
        Vector3 platesInitPos = new Vector3((row - 1) * -0.5f, (col - 1) * -0.5f, 1f);
        _plateList = new List<List<ToolPlate>>();
        for (int i = 0; i < row; i++)
        {
            _plateList.Add(new List<ToolPlate>());

            for (int j = 0; j < col; j++)
            {
                Vector3 platePosition = new Vector3(platesInitPos.x + _platesDistOffset * j, platesInitPos.y + _platesDistOffset * i, 1f);
                ToolPlate plate = Instantiate(_platePrefab, platePosition, Quaternion.identity, this.transform).GetComponent<ToolPlate>();
                _plateList[i].Add(plate);
            }
        }
    }

    public List<ToolPlate> GetPlates()
    {
        List<ToolPlate> toolPlates = new List<ToolPlate>(transform.childCount);

        for(int i = 0; i < transform.childCount; ++i)
        {
            toolPlates.Add(transform.GetChild(i).GetComponent<ToolPlate>());
        }

        return toolPlates;  
    }

    public Vector2Int GetPlateIndex(Vector3 platePosition)
    {
        Vector3 platesInitPos = new Vector3((PlatesNum.x - 1) * -0.5f, (PlatesNum.y - 1) * -0.5f, 1f);

        int rowIndex = (int)((platePosition.y - platesInitPos.y) / _platesDistOffset);
        int colIndex = (int)((platePosition.x - platesInitPos.x) / _platesDistOffset);

        return new Vector2Int(rowIndex, colIndex);
    }

    public ToolPlate GetPlateByIndex(Vector2Int plateIndex)
    {
        return _plateList[plateIndex.x][plateIndex.y];
    }
}
