using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mouse : Singleton<Mouse>
{
    private Vector3 _lastMousePosition;
    public bool IsDrawing { private get; set; }
    private void Start()
    {
        _lastMousePosition = Input.mousePosition;
    }
    private void FixedUpdate()
    {
        if (IsDrawing)
        {
            Vector3 newMousePosition = Input.mousePosition;

            float x = Mathf.Abs(newMousePosition.x - _lastMousePosition.x);
            float y = Mathf.Abs(newMousePosition.y - _lastMousePosition.y);

            Vector3 resultMousePosition = Vector3.zero;
            if (x > y)
            {
                resultMousePosition.x = newMousePosition.x;
            }
            else
            {
                resultMousePosition.y = newMousePosition.y;
            }

            _lastMousePosition = newMousePosition;
        }
    }

}
