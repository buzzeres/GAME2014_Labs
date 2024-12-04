using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    int _value;
    [SerializeField]
    int _maxHealth;

    [SerializeField]
    Slider _slider;

    bool _gameOverCondition;

    void Start()
    {
        _slider = GetComponentInChildren<Slider>();
        _slider.maxValue = _maxHealth;
        _slider.value = _maxHealth;
    }

    public void ResetHealthBar()
    {
        _slider.maxValue = _maxHealth;
        _slider.value = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _slider.value -= damage;

        if (_slider.value <= 0)
        {
            _slider.value = 0;
            if (_gameOverCondition)
            {
                // Handle game over logic here
                Debug.Log("Game Over");
            }
        }
    }


    public void HealHealth(int healAmount)
    {
        _slider.value += healAmount;
        if (_slider.value >= _maxHealth)
        {
            _slider.value = _maxHealth;
        }
    }
}
