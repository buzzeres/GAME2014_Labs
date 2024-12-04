using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    [SerializeField]
    bool IsSensing;
    [SerializeField]
    bool _LOS;

    PlayerBehavior _player;
    [SerializeField]
    LayerMask _layerMask;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSensing)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, _player.transform.position, _layerMask);

            Vector2 playerDirection = _player.transform.position - transform.position;
            float playerDirectionValue = (playerDirection.x > 0) ? 1 : -1;
            float enemyLookingDirectionValue = (transform.localScale.x > 0) ? -1 : 1;

            _LOS = (hit.collider.name == "Player") && playerDirectionValue == enemyLookingDirectionValue;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsSensing = true;
        }
    }

    public bool GetLOSStatus()
    {
        return _LOS;
    }


    private void OnDrawGizmos()
    {
        if (_player == null)
        {
            return;
        }

        Color color = (_LOS) ? Color.green : Color.red;

        if (IsSensing)
        {
            Debug.DrawLine(transform.position, _player.transform.position, color);
        }
    }
}

