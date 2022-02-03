using UnityEngine;

public class EventsManager : MonoBehaviour
{
    [SerializeField] private GameMode _gameMode;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private PlayerSound _playerSound;
    
    
    [SerializeField] private ObstaclesSpawner _obstaclesSpawner;

    [SerializeField] private HudController _hudController;
    [SerializeField] private HudSoundController _hudSoundController;
    [SerializeField] private MainOverlay _mainOverlay;
    [SerializeField] private ScoreOverlay _scoreOverlay;
    private void Awake()
    {
        _gameMode.OnCallConfig += _playerController.OnCallConfig;
        _obstaclesSpawner.OnPassObstacle += _gameMode.OnPassObstacle;

        _obstaclesSpawner.OnPositionChange += _playerController.OnPositionChange;
        _playerInput.OnTap += _playerController.OnTap;
        _playerController.OnPlayerHit += _gameMode.OnPlayerHit;

        _playerController.OnFlap += _playerAnimationController.OnJumpAnimation;
        _playerController.OnFall += _playerAnimationController.OnFallAnimation;
        
        //sound
        _playerController.OnFlap += _playerSound.OnFlapSound;
        _playerController.OnPlayerHit += _playerSound.OnSoundHitCall;
        
        //ui
        _gameMode.OnScoreReturn += _mainOverlay.OnScoreReturn;
        _scoreOverlay.OnRetry += _gameMode.OnRetry;
        _scoreOverlay.OnQuit += _gameMode.OnQuit;
        _gameMode.OnMedalsReturn += _scoreOverlay.OnMedalsReturn;
        _hudController.OnButtonPress += _hudSoundController.OnButtonPress;
    }
    
    private void OnDestroy()
    {
        _gameMode.OnCallConfig -= _playerController.OnCallConfig;
        _obstaclesSpawner.OnPassObstacle -= _gameMode.OnPassObstacle;
        
        _obstaclesSpawner.OnPositionChange -= _playerController.OnPositionChange;
        _playerInput.OnTap -= _playerController.OnTap;
        _playerController.OnPlayerHit -= _gameMode.OnPlayerHit;

        _playerController.OnFlap -= _playerAnimationController.OnJumpAnimation;
        _playerController.OnFall -= _playerAnimationController.OnFallAnimation;
        
        // sound
        _playerController.OnFlap -= _playerSound.OnFlapSound;
        _playerController.OnPlayerHit -= _playerSound.OnSoundHitCall;
        
        //ui
        _gameMode.OnScoreReturn -= _mainOverlay.OnScoreReturn;
        _scoreOverlay.OnRetry -= _gameMode.OnRetry;
        _scoreOverlay.OnQuit -= _gameMode.OnQuit;
        _gameMode.OnMedalsReturn -= _scoreOverlay.OnMedalsReturn;
        _hudController.OnButtonPress -= _hudSoundController.OnButtonPress;
    }
}
