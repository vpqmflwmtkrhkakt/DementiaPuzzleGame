using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClearUI : UIBase
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
