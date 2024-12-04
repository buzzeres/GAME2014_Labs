using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    PlayerDetection _playerDetection;
    [SerializeField]
    int _fireDelay = 30;

    [SerializeField]
    GameObject _bullet;
    bool _hasLOS;

    // Start is called before the first frame update
    void Start()
    {
        _playerDetection = GetComponent<PlayerDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        _hasLOS = _playerDetection.GetLOSStatus();
    }

    private void FixedUpdate()
    {
        if (_hasLOS && Time.frameCount % _fireDelay == 0)
        {
            Execute();
        }
    }

    public void Execute()
    {
        GameObject bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
    }
}
