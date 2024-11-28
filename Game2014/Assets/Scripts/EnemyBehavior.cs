using UnityEngine;

using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    float _speed = 5;

    [SerializeField]
    UnityEngine.Transform _baseCenterPoint;
    bool _IsGrounded;
    [SerializeField]
    float _groundCheckDistance;
    [SerializeField]
    UnityEngine.Transform _frontGroundPoint;
    bool _IsThereAnyFrontStep;

    [SerializeField]
    LayerMask _layerMask;

    [SerializeField]
    UnityEngine.Transform _frontObstaclePoint;
    bool IsThereAnyFrontObstacle;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        _IsGrounded = Physics2D.Linecast(_baseCenterPoint.position, _baseCenterPoint.position + Vector3.down * _groundCheckDistance, _layerMask);
        _IsThereAnyFrontStep = Physics2D.Linecast(_baseCenterPoint.position, _frontGroundPoint.position, _layerMask);
        IsThereAnyFrontObstacle = Physics2D.Linecast(_baseCenterPoint.position, _frontObstaclePoint.position, _layerMask);



        if (_IsGrounded && !_IsThereAnyFrontStep || IsThereAnyFrontObstacle)
            ChangeDirection();
        if (_IsGrounded)
            Move();
    }

    void Move()
    {
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
