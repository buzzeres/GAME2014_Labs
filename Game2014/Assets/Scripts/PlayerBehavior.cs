using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private float _horizontalForce;
    [SerializeField]
    private float _verticalForce;
    [SerializeField]
    private Transform _groundPoint;
    [SerializeField]
    private float _groundRadius;
    [SerializeField]
    private LayerMask _groundLayerMask;
    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float _airFactor;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _leftJoystickVerticalThreshold;
    [SerializeField]
    private float _horizontalSpeedLimit = 5.0f;
    [SerializeField]
    private float _deathlyFallSpeed = 5;

    Rigidbody2D _rigidbody;
    bool _isGrounded;
    Animator _animator;
    Joystick _leftJoystick;
    HealthBarController _healthBar;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _healthBar = FindObjectOfType<HealthBarController>();

        if (_healthBar == null)
        {
            Debug.LogError("HealthBarController component not found in the scene");
        }

        if (GameObject.Find("OnScreenControllers"))
        {
            _leftJoystick = GameObject.Find("LeftJoystick").GetComponent<Joystick>();
        }

        if (_groundPoint == null)
        {
            Debug.LogError("Ground Point is not assigned in the Inspector");
        }
    }

    void AnimatorStateControl()
    {
        if (_isGrounded)
        {
            if (Mathf.Abs(_rigidbody.velocity.x) > 0.1f)
            {
                _animator.SetInteger("State", (int)AnimationStates.Run);
                // Debug.Log("State: Run");
            }
            else
            {
                _animator.SetInteger("State", (int)AnimationStates.Idle);
                // Debug.Log("State: Idle");
            }
        }
        else
        {
            if (Mathf.Abs(_rigidbody.velocity.y) > _deathlyFallSpeed)
            {
                _animator.SetInteger("State", (int)AnimationStates.Fall);
                // Debug.Log("State: Fall");
            }
            else
            {
                _animator.SetInteger("State", (int)AnimationStates.Jump);
                // Debug.Log("State: Jump");
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_groundPoint != null)
        {
            _isGrounded = Physics2D.OverlapCircle(_groundPoint.position, _groundRadius, _groundLayerMask);
        }
        Move();
        Jump();
        AnimatorStateControl();
    }

    void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        if (_leftJoystick != null)
        {
            xInput = _leftJoystick.Horizontal;
            // Debug.Log(_leftJoystick.Horizontal + " " + _leftJoystick.Vertical);
        }

        if (xInput != 0.0f)
        {
            Vector2 force = Vector2.right * xInput * _horizontalForce;
            if (!_isGrounded)
            {
                force = new Vector2(force.x * _airFactor, force.y);
            }

            _rigidbody.AddForce(force);
            GetComponent<SpriteRenderer>().flipX = (force.x <= 0.0f);

            if (Mathf.Abs(_rigidbody.velocity.x) > _horizontalSpeedLimit)
            {
                float updatedXvalue = Mathf.Clamp(_rigidbody.velocity.x, -_horizontalSpeedLimit, _horizontalSpeedLimit);
                _rigidbody.velocity = new Vector2(updatedXvalue, _rigidbody.velocity.y);
            }
        }
    }

    void Jump()
    {
        float jumpPressed = Input.GetAxisRaw("Jump");
        if (_leftJoystick != null)
        {
            jumpPressed = _leftJoystick.Vertical;
        }

        if (_isGrounded && jumpPressed > _leftJoystickVerticalThreshold)
        {
            _rigidbody.AddForce(Vector2.up * _verticalForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IDamage damageDealer = collision.GetComponent<IDamage>();
            if (damageDealer != null)
            {
                if (_healthBar != null)
                {
                    _healthBar.TakeDamage(damageDealer.Damage());
                }
                else
                {
                    Debug.LogError("HealthBarController is not assigned");
                }
            }
        }
    }

    public void OnDrawGizmos()
    {
        if (_groundPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_groundPoint.position, _groundRadius);
        }
    }
}
