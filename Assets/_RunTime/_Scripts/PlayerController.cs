using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _forwardSpeed;
    
    [Header("Gravity")]
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _jumpForce;
    //rotation
    private float _rotationZ;
    private float _rotationZSpeed = 90;
    private float _flapRoration = 30;
    private float _SeedToRoration => _jumpForce * 0.3f;
    //animation
    [SerializeField] private Animator _PlayerAnimator;
    [SerializeField, Range(1,10)] private float _animationSpeed = 1;

    private void Update()
    {
        _ProcessMovements();
        transform.rotation = Quaternion.Euler(Vector3.forward * _rotationZ);
        transform.position += _velocity * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            _Jump();
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase.Equals(TouchPhase.Began))
            {
                _Jump();
            }
        }
    }

    private void _ProcessMovements()
    {
        _velocity.x = _forwardSpeed;
        _velocity.y -= _gravityScale * Time.deltaTime;
        if (_velocity.y < _SeedToRoration)
        {
            _rotationZ -= _rotationZSpeed * Time.deltaTime;
            _rotationZ = Mathf.Max(-90, _rotationZ);

            _JumpAnimation();
        }
    }

    private void _JumpAnimation()
    {
        float currentAnimationSpeed = _PlayerAnimator.speed;
        currentAnimationSpeed -= _animationSpeed * Time.deltaTime;
        _PlayerAnimator.speed = Mathf.Max(1, currentAnimationSpeed);
    }

    private void _Jump()
    {
        _velocity.y = _jumpForce;
        _rotationZ = _flapRoration;
        _PlayerAnimator.speed = _animationSpeed;
    }
}

/*

● GitHub - https://github.com/artrex-st
● Linkedin - https://www.linkedin.com/in/Arthur-St/
● Simmer - https://simmer.io/@ArtrexSt
● itch.io - https://artrexst.itch.io/
● GooglePlay - https://play.google.com/store/apps/developer?id=Arthur+Stefanelli

*/