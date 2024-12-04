using UnityEngine;

using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    float _speed = 5;

    [SerializeField]
    UnityEngine.Transform _baseCenterPoint;
    bool _isGrounded;
    [SerializeField]
    float _groundCheckDistance;
    [SerializeField]
    UnityEngine.Transform _frontGroundPoint;
    bool _isThereAnyFrontStep;

    [SerializeField]
    LayerMask _layerMask;

    PlayerDetection _playerDetector;

    Animator _animator;

    [SerializeField]
    UnityEngine.Transform _frontObstaclePoint;
    bool _isThereAnyFrontObstacle;

    // Start is called before the first frame update
    void Start()
    {
        _playerDetector = GetComponent<PlayerDetection>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.Linecast(_baseCenterPoint.position, _baseCenterPoint.position + Vector3.down * _groundCheckDistance, _layerMask);
        _isThereAnyFrontStep = Physics2D.Linecast(_baseCenterPoint.position, _frontGroundPoint.position, _layerMask);
        _isThereAnyFrontObstacle = Physics2D.Linecast(_baseCenterPoint.position, _frontObstaclePoint.position, _layerMask);

        if (_isGrounded && (!_isThereAnyFrontStep || _isThereAnyFrontObstacle))
        {
            ChangeDirection();
        }
        _animator.SetInteger("State", (int)AnimationStates.Idle);
        if (_isGrounded && !_playerDetector.GetLOSStatus())
        {
            Move();
        }
    }

    void Move()
    {
        _animator.SetInteger("State", (int)AnimationStates.Run);
        transform.position += Vector3.left * transform.localScale.x * _speed;
    }

    void ChangeDirection()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawLine(_baseCenterPoint.position, _baseCenterPoint.position + Vector3.down * _groundCheckDistance);
        Debug.DrawLine(_baseCenterPoint.position, _frontGroundPoint.position);
    }
}
