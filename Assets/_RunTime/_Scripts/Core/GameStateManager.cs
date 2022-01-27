public class GameStateManager
{
    public delegate void GameStateChangeHandler(GameStates newGameState);
    public event GameStateChangeHandler OnGameStateChanged;
    public GameStates CurrentGameState { get; private set; }

    private static GameStateManager _instance;
    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameStateManager();
            }
            return _instance;
        }
    }

    public void SetState(GameStates newGameState)
    {
        if (newGameState == CurrentGameState)
        {
            return;
        }
        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);
    }
}
