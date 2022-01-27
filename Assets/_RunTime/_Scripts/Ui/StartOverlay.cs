using UnityEngine;

public class StartOverlay : OverlayUI
{   
    private void Awake()
    {
        GetHudController();
    }
    private void OnEnable()
    {
        GameStateManager.Instance.SetState(GameStates.GAME_PAUSED);
    }
    public void BtnTapToStart()
    {
        hudController.OpenMenu(Menu.MAIN, gameObject);
    }
}
