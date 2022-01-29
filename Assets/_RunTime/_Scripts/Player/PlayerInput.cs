using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    public delegate void PlayerTap();
    public event PlayerTap OnTap;

    private bool _unlockInputs;
    private NewInputAction _playerControls;

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        _playerControls = new NewInputAction();
        _playerControls.PlayerBird.Tap.started += ctx => ProcessInput();
    }
    private void OnEnable()
    {
        _playerControls.Enable();
    }
    private void OnDisable()
    {
        _playerControls.Disable();
    }
    private void OnGameStateChanged(GameStates newGameState)
    {
        if (newGameState.Equals(GameStates.GAME_RUNNING))
        {
            _unlockInputs = true;
        }
        else
        {
            _unlockInputs = false;
        }
    }
    private void ProcessInput()
    {
        if (_unlockInputs)
        {
            OnTap?.Invoke();
        }
    }
    
}
