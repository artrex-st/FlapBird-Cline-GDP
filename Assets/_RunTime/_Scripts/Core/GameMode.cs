using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scores
{
    public int gold, silver, bronze, iron, lastScore;
}

public class GameMode : MonoBehaviour
{
    public delegate void GetGameConfig(GameConfig config);
    public event GetGameConfig OnCallConfig;
    public delegate void GetGameScore(int score);
    public event GetGameScore OnScoreReturn;
    public delegate void GetGameMedals(Scores scores, bool newScore, int medalIndex, bool record);
    public event GetGameMedals OnMedalsReturn;

    public const string scoreDataFileName = "Scores";
    private Scores _scores = new Scores();
    private int _currentScore;
    [SerializeField] private GameConfig _gameRuning, _gamePaused, _gameScore, _gameWaiting;
    
    public void OnPassObstacle()
    {
        _currentScore++;
        OnScoreReturn?.Invoke(_currentScore);
    }
    public void OnPlayerHit(Collider2D other)
    {
        _OnSavedScoreData();
        GameStateManager.Instance.SetState(GameStates.GAME_SCORE);
    }
    
    public void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Debug.Log("'WebGL Player' cannot be closed");
#else
        Application.Quit();
#endif
    }

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        SaveSystem.Initialize();
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
                OnCallConfig?.Invoke(_gameRuning);
                break;
            case GameStates.GAME_PAUSED:
                OnCallConfig?.Invoke(_gamePaused);
                break;
            case GameStates.GAME_SCORE:
                OnCallConfig?.Invoke(_gameScore);
                break;
            case GameStates.GAME_WAITING:
                _LoadScore();
                OnCallConfig?.Invoke(_gameWaiting);
                break;
            default:
                break;
        }
    }

    private void _LoadScore()
    {
        string jsonString = SaveSystem.Load(scoreDataFileName);
        if (jsonString != null)
        {
            _scores = JsonUtility.FromJson<Scores>(jsonString);
        }
    }

    private void _OnSavedScoreData()
    {   
        _CalcMedals();
        string jsonString = JsonUtility.ToJson(_scores);
        SaveSystem.Save(jsonString,scoreDataFileName);

    }

    private void _CalcMedals()
    {
        if (_currentScore >= _scores.gold)
        {
            if (_currentScore != _scores.gold)
            {
                _scores.iron = _scores.bronze;
                _scores.bronze = _scores.silver;
                _scores.silver = _scores.gold;
                _scores.gold = _currentScore;
            }
            _scores.lastScore = _currentScore;
            OnMedalsReturn?.Invoke(_scores, true, 3, true);
            return;
        }
        
        if (_currentScore >= _scores.silver)
        {
            if (_currentScore != _scores.silver)
            {
                _scores.iron = _scores.bronze;
                _scores.bronze = _scores.silver;
                _scores.silver = _currentScore;
            }
            _scores.lastScore = _currentScore;
            OnMedalsReturn?.Invoke(_scores, true, 2, false);
            return;
        }
        
        if (_currentScore >= _scores.bronze)
        {
            if (_currentScore != _scores.silver)
            {
                _scores.iron = _scores.bronze;
                _scores.bronze = _currentScore;
            }
            _scores.lastScore = _currentScore;
            OnMedalsReturn?.Invoke(_scores, true, 1, false);
            return;
        }
        
        if(_currentScore >= _scores.iron)
        {
            _scores.iron = _currentScore;
            _scores.lastScore = _currentScore;
            OnMedalsReturn?.Invoke(_scores, true, 0, false);
            return;
        }
        _scores.lastScore = _currentScore;
        OnMedalsReturn?.Invoke(_scores, false, 0, false); 
    }
}
