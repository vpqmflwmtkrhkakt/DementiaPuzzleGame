using System;

public class GameManager : Singleton<GameManager>
{
    private uint _remainConnection;
    public Action OnGameCleared;

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
                OnGameCleared?.Invoke();
            }
        }
    }

    public void PlusRemainingConnectionCount()
    {
        ++_remainConnection;
    }
}
