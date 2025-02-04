using System.Collections.Generic;
using UnityEngine;

public class PlateLoader
{
    private readonly int _platesDistOffset = 1;
    public Vector2Int PlatesNum { get; private set; }
    private List<List<Plate>> _plateList;

    private Plate _platePrefab;
    public PlateLoader(Plate platePrefab)
    {
        Debugger.CheckInstanceIsNullAndQuit(platePrefab);
        _platePrefab = platePrefab;
    }

    private void PlacePlates(int row, int col)
    {
        Vector3 platesInitPos = new Vector3((row - 1) * -0.5f, (col - 1) * -0.5f, 1f);
        _plateList = new List<List<Plate>>();

        int count = 1;
        for (int i = 0; i < row; i++)
        {
            _plateList.Add(new List<Plate>());

            for (int j = 0; j < col; j++)
            {
                Vector3 platePosition = new Vector3(platesInitPos.x + _platesDistOffset * j, platesInitPos.y + _platesDistOffset * i, 1f);
                Plate plate = UnityEngine.GameObject.Instantiate(_platePrefab, platePosition, Quaternion.identity).GetComponent<Plate>();
                plate.gameObject.name = "plate" + count++;
                _plateList[i].Add(plate);
            }
        }
    }
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
            foreach (List<Plate> subList in _plateList)
            {
                foreach (Plate plate in subList)
                {
                    UnityEngine.GameObject.Destroy(plate.gameObject);   
                }
                subList.Clear();
            }

            _plateList.Clear();
            _plateList = null;
        }
    }
    public Plate GetPlateByIndex(Vector2Int plateIndex)
    {
        return _plateList[plateIndex.x][plateIndex.y];
    }

}
