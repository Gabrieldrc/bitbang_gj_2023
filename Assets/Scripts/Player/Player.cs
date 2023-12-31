using System;
using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	public event Action<bool> OnSuperActivated;

	[SerializeField] private float _speed = 10f;
	[SerializeField] private float _jumpForce;
	[SerializeField] private float _throwForceScale = 1;
	[SerializeField] private Transform _groundCheck;
	[SerializeField] private float _groundCheckRadius;
	[SerializeField] private LayerMask _whatIsGround;
	[SerializeField] private float _jumpRealeseMod = 100f;
	[SerializeField] private EnemyDetector _enemyDetector;
	[SerializeField] private float _maxTimeToAttack = 2f;
	[SerializeField] private FloatValueSO _playerLiveSO;
	[SerializeField] private float _maxTimeToRecover = 2f;
	[SerializeField] private int _normalPlayerLayer;
	[SerializeField] private int _damagePlayerLayer;
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private float _maxTranformationTime;
	[SerializeField] private AudioClip _gritoSapucai;
	[SerializeField] private SpriteRenderer _arrowUI;
	[SerializeField] private PlayerAudioController _audioController;
	[SerializeField] private PlayerDamageController _playerDamageController;

	[Space(10), Header("Player Animations:")]
	[SerializeField]
	private Animator _anim;

	[SerializeField]
	private string _runningAnimParameter,
		_jumpAnimParameter,
		_kickAnimParameter,
		_groundedAnimParameter,
		_deathAnimTrigger,
		_transformedAnimParameter,
		_takeDamageAnimTrigger,
		_kickedAnimTrigger;

	private AudioSource _aSource;
	private Rigidbody2D _rigidbody;
	private float _hor_input = 0f;
	private bool _canMove = true;
	private float _gravityScale;
	private bool _isWaitingToAttack = false;
	private bool _canAttack = false;
	private float _timeAttackLeft = 0f;
	private Vector2 _targetDirection;
	private float _timeToRecover;
	private bool _isDamage = false;
	private bool _isSuperPowerful = false;
	private float _currentTranformationTime;
	private GameObject _currentPlatformOver;

	public bool IsSuperPowerful => _isSuperPowerful;

	void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_aSource = GetComponentInParent<AudioSource>();
		_gravityScale = _rigidbody.gravityScale;
		_timeAttackLeft = _maxTimeToAttack;
		_targetDirection = transform.position;
		_timeToRecover = _maxTimeToRecover;
		_currentTranformationTime = _maxTranformationTime;
	}

	private void OnEnable()
	{
		_enemyDetector.OnEnemyDetected += AttackHandler;
		_playerDamageController.OnTakeDamage += TakeDamageHandler;
	}

	private void OnDisable()
	{
		_enemyDetector.OnEnemyDetected -= AttackHandler;
		_playerDamageController.OnTakeDamage -= TakeDamageHandler;
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

	private void Update()
	{
		if (_isDamage)
		{
			_timeToRecover -= Time.deltaTime;
			if (_timeToRecover <= 0)
			{
				BackToNormal();
			}
		}
		else if (_isSuperPowerful)
		{
			_currentTranformationTime -= Time.deltaTime;
			if (_currentTranformationTime <= 0)
			{
				DeactivatePower();
			}
		}
	}

	private void LateUpdate()
	{
		float angulo = Mathf.Atan2(-_targetDirection.x, _targetDirection.y) * Mathf.Rad2Deg;
		_arrowUI.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo));
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PowerTrash"))
		{
			ActivatePower();
		}
		else if (collision.CompareTag("Enemy") && _isSuperPowerful)
		{
			var enemy = collision.GetComponent<Enemy>();
			enemy.Throw(_targetDirection * _throwForceScale);
		}
	}

	private void BackToNormal()
	{
		_isDamage = false;
		gameObject.layer = _normalPlayerLayer;
		_spriteRenderer.color = Color.white;
		_timeToRecover = _maxTimeToRecover;
		_anim.ResetTrigger(_takeDamageAnimTrigger);
	}

	public void MovementHandler(InputAction.CallbackContext context)
	{
		var mov = context.ReadValue<Vector2>();
		if (context.canceled)
		{
			_hor_input = 0f;
			_anim.SetBool(_runningAnimParameter, false);
		}

		if (context.performed)
		{
			_hor_input = mov.x;
			var floorCollider = GetFloorCollider();
			if (mov.y < -0.7f && floorCollider)
			{
				floorCollider.GetComponent<Platform>()?.Reverse();
			}
		}

		if (mov.x != 0)
		{
			_anim.SetBool(_runningAnimParameter, true);
			if (mov.x < 0)
			{
				transform.eulerAngles = new Vector3(0, 180, 0);
			}
			else
			{
				transform.eulerAngles = new Vector3(0, 0, 0);
			}
		}
	}

	public void TargetHandler(InputAction.CallbackContext context)
	{
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
			_audioController.PlayJumpSE();
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

		if (context.canceled)
		{
			_anim.SetBool(_jumpAnimParameter, false);
		}
	}


	private void AttackHandler()
	{
		StartCoroutine(CO_Ataque());
		_canMove = false;
		_rigidbody.velocity = Vector2.zero;
		_rigidbody.gravityScale = 0f;
		_anim.ResetTrigger(_kickedAnimTrigger);
	}

	private IEnumerator CO_Ataque()
	{
		_canMove = false;
		_rigidbody.velocity = Vector2.zero;
		_rigidbody.gravityScale = 0f;
		_isWaitingToAttack = true;
		_anim.SetBool(_kickAnimParameter, true);

		yield return new WaitUntil(CanInteraction);


		_canMove = true;
		_rigidbody.gravityScale = _gravityScale;
		_timeAttackLeft = _maxTimeToAttack;

		if (_canAttack)
		{
			_audioController.PlayKickSE();
			_enemyDetector.ThrowEnemy(_targetDirection * _throwForceScale);
			_anim.SetTrigger(_kickedAnimTrigger);

			AddJumpForce();
		}
		else
		{
			_anim.SetBool(_kickAnimParameter, false);
		}

		_isWaitingToAttack = false;
		_canAttack = false;
		_anim.SetBool(_kickAnimParameter, false);
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
		_anim.SetBool(_jumpAnimParameter, true);
		_anim.SetBool(_groundedAnimParameter, false);
	}

	private bool IsGrounded()
	{
		if (_rigidbody.velocity.y <= 0)
		{
			_anim.SetBool(_groundedAnimParameter, true);
			return GetFloorCollider();
		}

		return false;
	}

	private Collider2D GetFloorCollider()
	{
		return Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);
	}

	private void TakeDamageHandler(float damage)
	{
		_anim.SetTrigger(_takeDamageAnimTrigger);

		GameManager.instance.AddLive(-damage);
		_isDamage = true;
		gameObject.layer = _damagePlayerLayer;
		_spriteRenderer.color = Color.red;
	}


	private void ActivatePower()
	{
		_playerDamageController.active = false;
		OnSuperActivated?.Invoke(true);
		_aSource.PlayOneShot(_gritoSapucai);
		_isSuperPowerful = true;
		_anim.SetBool(_transformedAnimParameter, true);
	}

	private void DeactivatePower()
	{
		_playerDamageController.active = true;
		OnSuperActivated?.Invoke(false);
		Debug.Log("Normal man llama");
		_isSuperPowerful = false;
		_currentTranformationTime = _maxTranformationTime;
		_anim.SetBool(_transformedAnimParameter, false);
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