using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveablePlatformBehavior : MonoBehaviour
{
    [SerializeField]
    float _horizontalSpeed = 5;
    [SerializeField]
    float _horizontalDistance = 5;
    [SerializeField]
    float _verticalSpeed = 5;
    [SerializeField]
    float _verticalDistance = 5;

    [SerializeField]
    List<Transform> _pathList = new List<Transform>();

    List<Vector2> _destinitions = new List<Vector2>();
    int _destinationsIndex = 0;


    Vector2 _startPosition;
    Vector2 _endPosition;
    float _timer;
    [SerializeField]
    [Range(0f, 0.1f)]
    float _customMovementTimerChangeFactor;

    [SerializeField]
    PlatformMovementTypes _type;

    // Start is called before the first frame update
    void Start()
    {

        foreach(Transform t in _pathList)
        {
            _destinitions.Add(t.position);
        }

        _destinitions.Add(transform.position);
        _startPosition = transform.position;
        _endPosition = _destinitions[0];
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        if (_type == PlatformMovementTypes.CUSTOM)
        {
           // _timer += Time.deltaTime;
            if (_timer >= 1)
            {
                _timer = 0;
                _destinationsIndex++;
                if (_destinationsIndex >= _destinitions.Count)
                {
                    _destinationsIndex = 0;
                }
                _startPosition = transform.position;
                _endPosition = _destinitions[_destinationsIndex];
            }
            else
            {
                _timer += _customMovementTimerChangeFactor;
            }
        }
    }
    

    void Move()
    {
        switch (_type)
        {
            case PlatformMovementTypes.HORIZONTAL:
                transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance) + _startPosition.x, transform.position.y);
                break;
            case PlatformMovementTypes.VERTICAL:
                transform.position = new Vector2(transform.position.x, Mathf.PingPong(_verticalSpeed * Time.time, _verticalDistance) + _startPosition.y);
                break;
            case PlatformMovementTypes.DIAGONAL_RIGHT:
                transform.position = new Vector2(Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance) + _startPosition.x,
                                                 Mathf.PingPong(_verticalSpeed * Time.time, _verticalDistance) + _startPosition.y);
                break;
            case PlatformMovementTypes.DIAGONAL_LEFT:
                transform.position = new Vector2(_startPosition.x - Mathf.PingPong(_horizontalSpeed * Time.time, _horizontalDistance),
                                                 Mathf.PingPong(_verticalSpeed * Time.time, _verticalDistance) + _startPosition.y);
                break;
            case PlatformMovementTypes.CUSTOM:
                transform.position = Vector2.Lerp(_startPosition, _endPosition, _timer);
                break;
        }   
    }
}
