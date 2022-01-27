using UnityEngine;

public class EventsManager : MonoBehaviour
{
    [SerializeField] private GameMode _gameMode;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private ObstaclesSpawner _obstaclesSpawner;

    [SerializeField] private MainOverlay _mainOverlay;
    [SerializeField] private ScoreOverlay _scoreOverlay;
    private void Awake()
    {
        _gameMode.OnCallConfig += _playerController.OnCallConfig;
        _obstaclesSpawner.OnPassObstacle += _gameMode.OnPassObstacle;

        _obstaclesSpawner.OnPositionChange += _playerController.OnPositionChange;
        _playerInput.OnTap += _playerController.OnTap;
        _playerController.OnPlayerHit += _gameMode.OnPlayerHit;

        _playerController.OnJumpAnimation += _playerAnimationController.OnJumpAnimation;
        _playerController.OnFallAnimation += _playerAnimationController.OnFallAnimation;
        //ui
        _gameMode.OnScoreReturn += _mainOverlay.OnScoreReturn;
        _scoreOverlay.OnRetry += _gameMode.OnRetry;
    }
    
    private void OnDestroy()
    {
        _gameMode.OnCallConfig -= _playerController.OnCallConfig;
        _obstaclesSpawner.OnPassObstacle -= _gameMode.OnPassObstacle;
        
        _obstaclesSpawner.OnPositionChange -= _playerController.OnPositionChange;
        _playerInput.OnTap -= _playerController.OnTap;
        _playerController.OnPlayerHit -= _gameMode.OnPlayerHit;

        _playerController.OnJumpAnimation -= _playerAnimationController.OnJumpAnimation;
        _playerController.OnFallAnimation -= _playerAnimationController.OnFallAnimation;
        
        //ui
        _gameMode.OnScoreReturn -= _mainOverlay.OnScoreReturn;
        _scoreOverlay.OnRetry -= _gameMode.OnRetry;
    }
}
