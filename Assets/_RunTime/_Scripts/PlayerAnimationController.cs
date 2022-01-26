using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField, Range(1,10)] private float _animationSpeed = 1;

    private void Start()
    {
        _playerAnimator = _playerAnimator == null ? GetComponent<Animator>() : _playerAnimator;
    }

    public void OnJumpAnimation()
    {
        _playerAnimator.speed = _animationSpeed;
    }
    public void OnFallAnimation()
    {
        float currentAnimationSpeed = _playerAnimator.speed;
        currentAnimationSpeed -= _animationSpeed * Time.deltaTime;
        _playerAnimator.speed = Mathf.Max(1, currentAnimationSpeed);
    }
}
