using UnityEngine;
using TMPro;

public class MainOverlay : OverlayUI
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        GetHudController();
    }
    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStates newGameState)
    {
        if (newGameState.Equals(GameStates.GAME_SCORE))
        {
            hudController.OpenMenu(Menu.SCORE, gameObject);
            Debug.Log("Score call");
        }
    }

    private void OnEnable()
    {
        GameStateManager.Instance.SetState(GameStates.GAME_RUNNING);
    }
    public void BtnPause()
    {
        hudController.OpenMenu(Menu.PAUSE, gameObject);
    }
    public void OnScoreReturn(int score)
    {
        _scoreText.text = $"{score}";
    }

}
