using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private uint _remainConnection;

    private void Start()
    {
        LevelManager.Instance.LoadLevel("save");
    }

    public void InitRemainConnectionCount(uint remainConnection)
    {
        _remainConnection = remainConnection;
    }

    public bool IsGameCleared()
    {
        return _remainConnection == 0;
    }
    public void MinusRemainingConnectionCount()
    {
        if (_remainConnection > 0)
        {
            --_remainConnection;

            if (_remainConnection == 0)
            {
                UIManager.Instance.PopupUI("GameClearUI");
            }
        }
    }

    public void AddRemainingConnectionCount()
    {
        ++_remainConnection;
    }
}
