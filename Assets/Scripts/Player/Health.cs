using System;
using Feeling;
using UnityEngine;

public class Health : MonoBehaviour, IAttackeble
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _curHealth;

    public event Action Died;
    public event Action<float, float> HealthChanged;

    public float CurrentHealth
    {
        get => _curHealth;
        private set
        {
            _curHealth = value;
            HealthChanged?.Invoke(_curHealth, _maxHealth);
            if (_curHealth <= 0)
            {
                _curHealth = 0;
                Died?.Invoke();
            }
        }
    }

    public float MaxHealth => _maxHealth;

    public void Healing()
    {
        AudioEffects.Instance.AudioHealting.Play();
        _curHealth += 50;
        if (_curHealth > _maxHealth)
            _curHealth = _maxHealth;
    }

    public void SetDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    public void IncreaseMaxHealth(float newMaxHealth)
    {
        _maxHealth += newMaxHealth;
    }
}