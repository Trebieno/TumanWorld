using System;
using UnityEngine;
using UnityEngine.Audio;

public class Economic : MonoBehaviour
{
    [SerializeField] private float _moneyMultiplier = 1;
    [SerializeField] private float _money;

    public event Action<float> MoneyChanged;

    public float Money
    {
        get => _money;
        set
        {
            _money = value;
            MoneyChanged?.Invoke(_money);
        }
    }

    public float MoneyMultiplier => _moneyMultiplier;

    public void IncreaseMoneyMultiplier(float step)
    {
        _moneyMultiplier += (_moneyMultiplier / 100) * step;
    }

    public void SetMultiplier(float step)
    {
        _moneyMultiplier = step;
    }
}