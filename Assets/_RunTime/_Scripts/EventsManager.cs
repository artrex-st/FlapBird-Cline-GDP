using UnityEngine;

public class EventsManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerAnimationController _playerAnimationController;
    [SerializeField] private ObstaclesSpawner _obstaclesSpawner;
    private void Start()
    {
        _obstaclesSpawner.OnPositionChange += _playerController.OnPositionChange;
        _playerInput.OnTap += _playerController.OnTap;

        _playerController.OnJumpAnimation += _playerAnimationController.OnJumpAnimation;
        _playerController.OnFallAnimation += _playerAnimationController.OnFallAnimation;
    }
    
    private void OnDestroy()
    {
        _obstaclesSpawner.OnPositionChange -= _playerController.OnPositionChange;
        _playerInput.OnTap -= _playerController.OnTap;

        _playerController.OnJumpAnimation -= _playerAnimationController.OnJumpAnimation;
        _playerController.OnFallAnimation -= _playerAnimationController.OnFallAnimation;
        
    }
}
