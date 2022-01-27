using UnityEngine;

public class GameMode : MonoBehaviour
{
    public delegate GameConfig GetGameConfig();
    public event GetGameConfig OnCallConfig;
    [SerializeField] private int _score = 0;
    [SerializeField] private GameConfig _gameConfig;
    
    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    public void OnPassObstacle()
    {
        _score++;
    }
    private void OnGameStateChanged(GameStates newGameState)
    {
        switch (newGameState)
        {
            case GameStates.GAME_RUNNING:
                Debug.Log("Runing");
                break;
            case GameStates.GAME_PAUSED:
                Debug.Log("Pause");
                break;
            default:
                break;
        }
    }
}
