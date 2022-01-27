using UnityEngine;

public class PauseOverlay : OverlayUI
{
    private void OnEnable()
    {
        GameStateManager.Instance.SetState(GameStates.GAME_PAUSED);
    }
    public void BtnResume()
    {
        hudController.OpenMenu(Menu.MAIN, gameObject);
    }
}
