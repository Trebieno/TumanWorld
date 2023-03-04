using System;
using Feeling;
using UnityEngine;

public class Health : MonoBehaviour, IAttackeble
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _curHealth;

    [SerializeField] private AudioSource _audioHealting;

    public event Action Died;
    public event Action<float, float> HealthChanged;

    public float CurrentHealth
    {
        get => _curHealth;
        set
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

    private void Start()
    {
        _audioHealting.clip = AudioEffects.Instance.AudioHealting;
    }

    public void Healing()
    {
        _audioHealting.Play();
        CurrentHealth += 50;
        // HealthChanged?.Invoke(_curHealth, _maxHealth);
        if (_curHealth > _maxHealth)
            _curHealth = _maxHealth;
    }

    public void SetDamage(float damage)
    {
        if(!_audioHealting.isPlaying)
            _audioHealting.Play();
        CurrentHealth -= damage;
    }

    public void IncreaseMaxHealth(float newMaxHealth)
    {
        _maxHealth += (_maxHealth / 100) * newMaxHealth;
        HealthChanged?.Invoke(_curHealth, _maxHealth);
    }

    public void SetHealth(float health)
    {
        _maxHealth = health;
        if(_curHealth >= _maxHealth)
            _curHealth = _maxHealth;
    }
}