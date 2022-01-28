using UnityEngine;

public class StartOverlay : OverlayUI
{   
    private void Awake()
    {
        GetHudController();
    }
    private void Start()
    {
        GameStateManager.Instance.SetState(GameStates.GAME_WAITING);
    }
    public void BtnTapToStart()
    {
        hudController.OpenMenu(Menu.MAIN, gameObject);
    }
}
