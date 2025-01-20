using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateManager : Singleton<PlateManager>
{
    private readonly Vector2 _platesInitPos = new Vector2(-5f, -4.5f);
    private readonly Int32 _platesDistOffset = 1;
    private readonly Int32 _maxColumnPlatesNum = 10;
    private readonly Int32 _maxRowPlatesNum = 11;

    [SerializeField]
    private Plate _platePrefab;

    private void Start()
    {
        Debugger.CheckInstanceIsNull(_platePrefab);

        PlacePlates();
    }

    private void PlacePlates()
    {
        for(int i = 0; i < _maxColumnPlatesNum; i++)
        {
            for(int j = 0; j < _maxRowPlatesNum; j++)
            {
                Vector3 platePosition = new Vector3(_platesInitPos.x + _platesDistOffset * j, _platesInitPos.y + _platesDistOffset * i, 1f);
                Plate plate = Instantiate(_platePrefab, platePosition, Quaternion.identity, this.transform);
            }
        }
    }

}
