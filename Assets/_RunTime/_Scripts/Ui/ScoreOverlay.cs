using UnityEngine;

public class ScoreOverlay : OverlayUI
{
    public delegate void CallRetry();
    public event CallRetry OnRetry;
    private void OnEnable()
    {
        //GameStateManager.Instance.SetState(GameStates.GAME_SCORE);
    }
    public void BtnRestart()
    {
        OnRetry?.Invoke();
    }
}
