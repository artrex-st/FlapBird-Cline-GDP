using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void AnimatorHandler();
    public event AnimatorHandler OnJumpAnimation, OnFallAnimation;
    public delegate void PlayerHitEventHandler(Collider2D hited);
    public event PlayerHitEventHandler OnPlayerHit;

    [SerializeField] private GameConfig _playerConfig;
    [SerializeField]private Vector3 _velocity;
    private float _rotationZ;
    private float _SpeedToRorate => _playerConfig.JumpForce * 0.3f;
    private bool _IsRunning;
    public void OnCallConfig(GameConfig config)
    {
        _playerConfig = config;
    }
    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStates newGameState)
    {
        switch (newGameState)
        {
            case GameStates.GAME_RUNNING:
                _IsRunning = true;
                break;
            case GameStates.GAME_PAUSED:
                _IsRunning = false;
                break;
            case GameStates.GAME_SCORE:
                _IsRunning = true;
                break;
            case GameStates.GAME_WAITING:
                _velocity.y = _playerConfig.JumpForce;
                _rotationZ = _playerConfig.FlapRoration;
                _IsRunning = false;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        _ProcessMovements();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnPlayerHit?.Invoke(other);
    }

    public void OnTap()
    {
        _velocity.y = _playerConfig.JumpForce;
        _rotationZ = _playerConfig.FlapRoration;
        
        OnJumpAnimation?.Invoke();
    }
    public Vector3 OnPositionChange()
    {
        return transform.position;
    }
    private void _ProcessMovements()
    {
        if (_IsRunning)
        {
            _velocity.x = _playerConfig.ForwardSpeed;
            _velocity.y -= _playerConfig.GravityScale * Time.deltaTime;
            if (_velocity.y < _SpeedToRorate)
            {
                _rotationZ -= _playerConfig.RotationZSpeed * Time.deltaTime;
                _rotationZ = Mathf.Max(-90, _rotationZ);

                OnFallAnimation?.Invoke();
            }

            transform.rotation = Quaternion.Euler(Vector3.forward * _rotationZ);
            transform.position += _velocity * Time.deltaTime;
        }
    }
}

/*

● GitHub - https://github.com/artrex-st
● Linkedin - https://www.linkedin.com/in/Arthur-St/
● Simmer - https://simmer.io/@ArtrexSt
● itch.io - https://artrexst.itch.io/
● GooglePlay - https://play.google.com/store/apps/developer?id=Arthur+Stefanelli

*/