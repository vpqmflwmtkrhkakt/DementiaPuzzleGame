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
                // ���� Ŭ����
                Debug.Log("Game Clear!");
                // TODO : ���� Ŭ���� UI ��쵵��
            }
        }
    }

    public void SetDisconnected()
    {
        ++_remainConnection;
    }
}
