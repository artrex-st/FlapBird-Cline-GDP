using UnityEngine;

public class PauseOverlay : OverlayUI
{

    public delegate void HudSoundHandler(); // todo
    public event HudSoundHandler OnButtonPress; // todo
    private void OnEnable()
    {
        GameStateManager.Instance.SetState(GameStates.GAME_PAUSED);
    }
    public void BtnResume()
    {
        hudController.OpenMenu(Menu.MAIN, gameObject);
        hudController.InvokePressButton();
    }
}
