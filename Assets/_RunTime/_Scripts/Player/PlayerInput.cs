using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public delegate void PlayerTap();
    public event PlayerTap OnTap;
    private bool _unlockInputs;

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
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

    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0) && _unlockInputs)
        {
            OnTap?.Invoke();
        }

        if (Input.touchCount > 0 && _unlockInputs)
        {
            if (Input.GetTouch(0).phase.Equals(TouchPhase.Began))
            {
                OnTap?.Invoke();
            }
        }
    }
    
}
