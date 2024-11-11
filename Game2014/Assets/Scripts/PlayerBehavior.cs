using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private float _horizontalForce;
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _verticalForce;
    private bool _isGrounded;
    [SerializeField]
    private Transform _groundPoint;
    [SerializeField]
    private float _groundRadius;
    [SerializeField]
    private LayerMask _groundLayerMask;

    private Animator _animator; // Changed from Animation to Animator

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float _airFactor;

    private Joystick _leftJoystick;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float _leftJoystickVerticalThreshold;

    [SerializeField]
    private float _horizontalSpeedLimit = 5.0f;

    [SerializeField]
    float _deathlyFallSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>(); // Changed from Animation to Animator

        if (GameObject.Find("OnScreenControllers"))
        {
            _leftJoystick = GameObject.Find("LeftJoystick").GetComponent<Joystick>();
        }

        // Check if _groundPoint is assigned
        if (_groundPoint == null)
        {
            Debug.LogError("Ground Point is not assigned in the Inspector");
        }
    }

    void AnimatorStateControl()
    {
        if (_isGrounded)
        {
            if (Mathf.Abs(_rigidbody.velocity.x) > 0.2f)
            {
                _animator.SetInteger("State", (int)AnimationStates.Run);
                Debug.Log("State: Run");
            }
            else
            {
                _animator.SetInteger("State", (int)AnimationStates.Idle);
                Debug.Log("State: Idle");
            }
        }
        else
        {
            if (Mathf.Abs(_rigidbody.velocity.y) > _deathlyFallSpeed)
            {
                _animator.SetInteger("State", (int)AnimationStates.Fall);
                Debug.Log("State: Fall");
            }
            else
            {
                _animator.SetInteger("State", (int)AnimationStates.Jump);
                Debug.Log("State: Jump");
            }
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if _groundPoint is assigned before using it
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
            Debug.Log(_leftJoystick.Horizontal + " " + _leftJoystick.Vertical);
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

    public void OnDrawGizmos()
    {
        if (_groundPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_groundPoint.position, _groundRadius);
        }
    }
}
