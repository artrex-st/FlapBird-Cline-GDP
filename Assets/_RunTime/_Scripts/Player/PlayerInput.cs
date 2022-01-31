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
            _playerControls.PlayerBird.Tap.started += ctx => ProcessInput();
        }
        else
        {
            _playerControls.PlayerBird.Tap.started -= ctx => ProcessInput();
        }
    }
    private void ProcessInput()
    {
        OnTap?.Invoke();
    }
    
}
