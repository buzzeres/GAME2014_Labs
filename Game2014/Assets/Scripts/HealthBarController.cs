using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    int _value;
    [SerializeField]
    int _maxHealth;

    Slider _slider;

    bool _gameOverCondition;

    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _maxHealth;
        _slider.value = _maxHealth;
    }

    public void ResetHealthBar()
    {
        _slider.maxValue = _maxHealth;
        _slider.value = _maxHealth;
    }


    public void TakeDamge(int Damage)
    {
        _slider.value -= Damage;

        if (_slider.value <= 0)
        {
           
            if (_gameOverCondition == true)
            {
                // Game Over
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
