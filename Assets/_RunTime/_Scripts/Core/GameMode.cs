using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    public delegate void GetGameConfig(GameConfig config);
    public event GetGameConfig OnCallConfig;
    public delegate void GetGameScore(int score);
    public event GetGameScore OnScoreReturn;

    [SerializeField] private int _score = 0;
    [SerializeField] private GameConfig _gameRuning, _gamePaused, _gameScore, _gameWaiting;
    
    public void OnPassObstacle()
    {
        _score++;
        OnScoreReturn?.Invoke(_score);
    }
    public void OnPlayerHit(Collider2D other)
    {
        GameStateManager.Instance.SetState(GameStates.GAME_SCORE);
    }
    
    public void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStates newGameState)
    {
        switch (newGameState)
        {
            case GameStates.GAME_RUNNING:
                Debug.Log("Runing");
                OnCallConfig?.Invoke(_gameRuning);
                break;
            case GameStates.GAME_PAUSED:
                Debug.Log("Pause");
                OnCallConfig?.Invoke(_gamePaused);
                break;
            case GameStates.GAME_SCORE:
                Debug.Log("Score");
                OnCallConfig?.Invoke(_gameScore);
                break;
            case GameStates.GAME_WAITING:
                Debug.Log("Waiting");
                OnCallConfig?.Invoke(_gameWaiting);
                break;
            default:
                break;
        }
    }
}
