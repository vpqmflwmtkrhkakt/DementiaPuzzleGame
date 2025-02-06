using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearUI : BaseUI
{
    private Button _retryBtn;
    private void Awake()
    {
        _retryBtn = transform.Find("RetryBtn").GetComponent<Button>();
        
        Debug.Assert( _retryBtn != null, "Retry Button is missing");
        
        _retryBtn.onClick.AddListener(RetryThisRound);

    }

    private void RetryThisRound()
    {
        // fix : �̰� ui���� ���� �θ��°� ��...
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
