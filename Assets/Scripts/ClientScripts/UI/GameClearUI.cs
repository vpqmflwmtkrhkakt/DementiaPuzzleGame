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
        // fix : 이걸 ui에서 직접 부르는건 좀...
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
