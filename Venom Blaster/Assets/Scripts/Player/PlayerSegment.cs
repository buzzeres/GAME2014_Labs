using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSegment : MonoBehaviour
{
    public Transform target;  // The segment this segment follows
    public float followSpeed = 5f;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        // Smoothly follow the target
        Vector3 newPosition = Vector3.Lerp(transform.position, target.position + offset, followSpeed * Time.deltaTime);
        transform.position = newPosition;
    }
}
