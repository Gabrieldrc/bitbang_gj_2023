using System.Collections;
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
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private float _maxTimeToAttack = 2f;

    private Rigidbody2D _rigidbody;
    private float _hor_input = 0f;
    private bool _canMove = true;
    private float _gravityScale;
    private bool _isWaitingToAttack = false;
    private bool _canAttack = false;
    private float _timeAttackLeft = 0f;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _gravityScale = _rigidbody.gravityScale;
        _timeAttackLeft = _maxTimeToAttack;
    }

    private void OnEnable()
    {
        _enemyDetector.OnEnemyDetected += AttackHandler;
    }

    private void OnDisable()
    {
        _enemyDetector.OnEnemyDetected -= AttackHandler;
    }

    private void AttackHandler()
    {
        StartCoroutine(CO_Ataque());
        _canMove = false;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0f;
    }

    private IEnumerator CO_Ataque()
    {
        _canMove = false;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0f;
        _isWaitingToAttack = true;

        yield return new WaitUntil(CanInteraction);

        if (_canAttack)
        {
            Debug.Log("Atacaste");
        }
        else
        {
            Debug.Log("No atacaste");
        }

        _canMove = true;
        _rigidbody.gravityScale = _gravityScale;
        _timeAttackLeft = _maxTimeToAttack;
        _canAttack = false;
        _isWaitingToAttack = false;
    }

    private bool CanInteraction()
    {
        _timeAttackLeft -= Time.deltaTime;
        return _canAttack || _timeAttackLeft <= 0;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            _rigidbody.velocity =
                new Vector2(Mathf.Clamp(Mathf.Abs(_hor_input * _speed), 0f, _speed) * (_hor_input > 0 ? 1 : -1),
                    _rigidbody.velocity.y);
        }
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

        if (_isWaitingToAttack && context.performed)
        {
            _canAttack = true;
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