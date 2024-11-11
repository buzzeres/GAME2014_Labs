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

    [SerializeField]
    [Range(0.0f, 10.0f)]
    float _airFactor;

    Joystick _leftJoystick;
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float _leftJoystickVerticalTreshold;

    [SerializeField]
    private float _horizontalSpeedLimit = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

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

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check if _groundPoint is assigned before using it
        if (_groundPoint != null)
        {
            _isGrounded = Physics2D.OverlapCircle(_groundPoint.position, _groundRadius, _groundLayerMask);
        }
        Move();
        jump();
    }

    void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        if (_leftJoystick)
        {
            xInput = _leftJoystick.Horizontal;
            Debug.Log(_leftJoystick.Horizontal + " " + _leftJoystick.Vertical);
        }

        if (xInput != 0.0f)
        {
            Vector2 force = Vector2.right * xInput * _horizontalForce;
            if (!_isGrounded)
            {
                force *= _airFactor;
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

    void jump()
    {
        var jumpPressed = Input.GetAxisRaw("Jump");
        if(_leftJoystick)
        {
            jumpPressed = _leftJoystick.Vertical;
        }

        if (_isGrounded && jumpPressed > _leftJoystickVerticalTreshold)
        {
            _rigidbody.AddForce(Vector2.up * _verticalForce);
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
