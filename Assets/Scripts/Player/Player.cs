using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	[SerializeField] private float _speed = 10f;
	[SerializeField] private float _jumpForce;
	[SerializeField] private float _throwForceScale = 1;
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
	private Vector2 _targetDirection;


	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_gravityScale = _rigidbody.gravityScale;
		_timeAttackLeft = _maxTimeToAttack;
		_targetDirection = transform.position;
	}

	private void OnEnable()
	{
		_enemyDetector.OnEnemyDetected += AttackHandler;
	}

	private void OnDisable()
	{
		_enemyDetector.OnEnemyDetected -= AttackHandler;
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

	public void TargetHandler(InputAction.CallbackContext context)
	{
		// if (!_isWaitingToAttack) return;
		if (!context.performed) return;
		var device = context.control.device;

		if (device is Gamepad)
		{
			_targetDirection = context.ReadValue<Vector2>().normalized;
		}
		else if (device is Mouse)
		{
			var mousePos = context.ReadValue<Vector2>();
			mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
			_targetDirection = (mousePos - (Vector2)transform.position).normalized;
		}
	}

	public void JumpHandler(InputAction.CallbackContext context)
	{

		if (context.performed && IsGrounded())
		{
			AddJumpForce();
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


		_canMove = true;
		_rigidbody.gravityScale = _gravityScale;
		_timeAttackLeft = _maxTimeToAttack;

		if (_canAttack)
		{
			Debug.Log("Atacaste");
			_enemyDetector.ThrowEnemy(_targetDirection * _throwForceScale);
			AddJumpForce();
		}
		else
		{
			Debug.Log("No atacaste");
		}

		_isWaitingToAttack = false;
		_canAttack = false;
	}

	private bool CanInteraction()
	{
		_timeAttackLeft -= Time.deltaTime;
		return _canAttack || _timeAttackLeft <= 0;
	}

	private void AddJumpForce()
	{
		var velocity = _rigidbody.velocity;
		velocity.y = _jumpForce;
		_rigidbody.velocity = velocity;
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

		Gizmos.color = Color.red;
		Vector2 position = transform.position;
		Gizmos.DrawLine(position, position + (_targetDirection * 2));
	}
#endif
}