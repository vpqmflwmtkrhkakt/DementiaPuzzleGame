using System.Collections.Generic;
using UnityEngine;

public class PlateCreator : MonoBehaviour
{
    [SerializeField]
    private ToolPlate _platePrefab;
    private readonly int _platesDistOffset = 1;
    public Vector2Int PlatesNum { get; private set; }
    private List<List<ToolPlate>> _plateList;
    public void CreatePlates(int row, int col)
    {
        RemovePlates();
        PlacePlates(row, col);
        PlatesNum = new Vector2Int(row, col);
    }
    private void RemovePlates()
    {
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

    private void PlacePlates(int row, int col)
    {
        Vector3 platesInitPos = new Vector3((row - 1) * -0.5f, (col - 1) * -0.5f, 1f);
        _plateList = new List<List<ToolPlate>>();
        for (int i = 0; i < row; i++)
        {
            _plateList.Add(new List<ToolPlate>());

            for (int j = 0; j < col; j++)
            {
                Vector3 platePosition = new Vector3(platesInitPos.x + _platesDistOffset * j, platesInitPos.y + _platesDistOffset * i, 1f);
                ToolPlate plate = Instantiate(_platePrefab, platePosition, Quaternion.identity).GetComponent<ToolPlate>();
                _plateList[i].Add(plate);
            }
        }
    }

    public List<List<ToolPlate>> GetPlateList()
    {
        return _plateList;
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
