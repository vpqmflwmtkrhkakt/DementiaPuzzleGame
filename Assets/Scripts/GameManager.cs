using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private ushort _remainConnection;

    private void Start()
    {
        LevelManager.Instance.LoadLevel("save");
    }
    public void SetConnected()
    {
        if (_remainConnection > 0)
        {
            --_remainConnection;

            if (_remainConnection == 0)
            {
                // 게임 클리어
                Debug.Log("Game Clear!");
                // TODO : 게임 클리어 UI 띄우도록
            }
        }
    }

    public void SetDisconnected()
    {
        ++_remainConnection;
    }
}
