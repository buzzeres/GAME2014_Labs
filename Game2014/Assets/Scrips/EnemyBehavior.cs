using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;       
    [SerializeField]
    private float _horizontalSpeed = 1.0f; 

    private Boundary _horizontalBoundary; 
    private Boundary _verticalBoundary;   

    private bool _movingRight = true; 

    private GameManager gameManager; 

    void Start()
    {
        // Get a reference to the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();

        // Set initial position to a random horizontal point at the top of the screen
        SetRandomProperties();
        transform.position = new Vector3(Random.Range(_horizontalBoundary.min, _horizontalBoundary.max), _verticalBoundary.max, 0);
    }

    void Update()
    {
        // Move the enemy down
        transform.position += Vector3.down * _speed * Time.deltaTime;

       
        if (_movingRight)
        {
            transform.position += Vector3.right * _horizontalSpeed * Time.deltaTime;

            if (transform.position.x > _horizontalBoundary.max)
            {
                _movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * _horizontalSpeed * Time.deltaTime;

            if (transform.position.x < _horizontalBoundary.min)
            {
                _movingRight = true;
            }
        }

        // If the enemy moves past the bottom vertical boundary, notify the GameManager and respawn this enemy with new properties
        if (transform.position.y < _verticalBoundary.min)
        {
            Respawn();
        }
    }

    public void SetBoundaries(Boundary horizontal, Boundary vertical)
    {
        _horizontalBoundary = horizontal;
        _verticalBoundary = vertical;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the enemy collides with the player, destroy the enemy and update the score
        if (other.CompareTag("Player"))
        {
            gameManager.EnemyDestroyed(gameObject);
        }
    }

    
    void Respawn()
    {
        SetRandomProperties();

        transform.position = new Vector3(Random.Range(_horizontalBoundary.min, _horizontalBoundary.max), _verticalBoundary.max, 0);
    }

    private void SetRandomProperties()
    {
    //    float randomSize = Random.Range(1.0f, 1.5f);
    //    transform.localScale = new Vector3(randomSize, randomSize, 1);

        Color randomColor = new Color(Random.value, Random.value, Random.value);
        GetComponent<SpriteRenderer>().color = randomColor;

        _speed = Random.Range(1.0f, 4.0f);
        _horizontalSpeed = Random.Range(0.5f, 2.5f);
    }
}
