using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health _health;
    private Slider _healthSlider;

    private void Awake()
    {
        _healthSlider = GetComponent<Slider>();
        _health.HealthChanged += Health_OnHealthChanged;
        _healthSlider.maxValue = _health.MaxHealth;
        _healthSlider.value = _health.CurrentHealth;
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
