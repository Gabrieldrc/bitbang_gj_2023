using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private float _jumpRealeseMod = 100f;

    private Rigidbody2D _rigidbody;
    private float _hor_input = 0f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity =
            new Vector2(Mathf.Clamp(Mathf.Abs(_hor_input * _speed), 0f, _speed) * (_hor_input > 0 ? 1 : -1),
                _rigidbody.velocity.y);
    }

    public void MovementHandler(InputAction.CallbackContext context)
    {
        var mov = context.ReadValue<Vector2>();
        if (context.canceled)
        {
            _hor_input = 0f;
        }

        if (context.performed)
        {
            _hor_input = mov.x;
        }
    }

    public void JumpHandler(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            var velocity = _rigidbody.velocity;
            velocity.y = _jumpForce;
            _rigidbody.velocity = velocity;
        }

        if (context.canceled && _rigidbody.velocity.y > 0f)
        {
            var velocity = _rigidbody.velocity;
            velocity.y /= _jumpRealeseMod;
            _rigidbody.velocity = velocity;
        }
    }

    private bool IsGrounded()
    {
        if (_rigidbody.velocity.y <= 0)
        {
            return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);
        }

        return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_groundCheck.position, _groundCheckRadius);
    }
#endif
}
