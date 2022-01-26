using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void AnimatorHandler();
    public event AnimatorHandler OnJumpAnimation, OnFallAnimation;
    [SerializeField] private float _forwardSpeed;
    [Header("Gravity")]
    [SerializeField] private float _gravityScale;
    [SerializeField] private float _jumpForce;
    private Vector3 _velocity;
    private bool _isDead = false;
    //rotation
    private float _rotationZ;
    private float _rotationZSpeed = 90;
    private float _flapRoration = 30;
    private float _SeedToRoration => _jumpForce * 0.3f;
    //

    private void Update()
    {
        _ProcessMovements();

        transform.rotation = Quaternion.Euler(Vector3.forward * _rotationZ);
        transform.position += _velocity * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _OnDeath();
    }

    public void OnTap()
    {
        if (!_isDead)
        {
            _velocity.y = _jumpForce;
            _rotationZ = _flapRoration;
            OnJumpAnimation?.Invoke();
        }
    }
    public Vector3 OnPositionChange()
    {
        return transform.position;
    }

    private void _ProcessMovements()
    {
        _velocity.x = _forwardSpeed;
        _velocity.y -= _gravityScale * Time.deltaTime;
        if (_velocity.y < _SeedToRoration)
        {
            _rotationZ -= _rotationZSpeed * Time.deltaTime;
            _rotationZ = Mathf.Max(-90, _rotationZ);

            OnFallAnimation?.Invoke();
        }
    }
    private void _OnDeath()
    {
        _forwardSpeed = 0;
        _jumpForce = 0;
        _isDead = true;
    }
}

/*

● GitHub - https://github.com/artrex-st
● Linkedin - https://www.linkedin.com/in/Arthur-St/
● Simmer - https://simmer.io/@ArtrexSt
● itch.io - https://artrexst.itch.io/
● GooglePlay - https://play.google.com/store/apps/developer?id=Arthur+Stefanelli

*/