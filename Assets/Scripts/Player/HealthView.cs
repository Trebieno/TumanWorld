using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _healthSlider;

    private void Awake()
    {
        _health.HealthChanged += Health_OnHealthChanged;
        _healthSlider.value = _health.CurrentHealth;
        _healthSlider.maxValue = _health.MaxHealth;

    }

    private void OnDestroy()
    {
        _health.HealthChanged -= Health_OnHealthChanged;
    }

    private void Health_OnHealthChanged(float curHealth, float maxHealth)
    {
        _healthSlider.value = curHealth;
        _healthSlider.maxValue = maxHealth;
    }
}
