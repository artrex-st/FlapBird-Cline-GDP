using UnityEngine;

public class HudController : MonoBehaviour
{

    private GameObject _lastMenu;
    [SerializeField] private GameObject _startOverlay, _mainOverlay, _pauseOverlay, _gameOverOverlay;

    private void Awake()
    {
        _startOverlay.SetActive(true);
        
        _mainOverlay.SetActive(false);
        _pauseOverlay.SetActive(false);
        _gameOverOverlay.SetActive(false);
    }

    public void OpenMenu(Menu menuOpened, GameObject callingMenu)
    {
        switch (menuOpened)
        {
            case Menu.START:
                _startOverlay.SetActive(true);
                break;
            case Menu.MAIN:
                _mainOverlay.SetActive(true);
                break;
            case Menu.PAUSE:
                _pauseOverlay.SetActive(true);
                break;
            case Menu.SCORE:
                _gameOverOverlay.SetActive(true);
                break;
            case Menu.CLOSE:
                _lastMenu.SetActive(true);
                break;
            default:
                break;
        }
        _lastMenu = callingMenu;
        callingMenu.SetActive(false);
    }
}
