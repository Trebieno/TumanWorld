using System;
using UnityEngine;

public class Economic : MonoBehaviour
{
    [SerializeField] private int _moneyMultiplier;
    [SerializeField] private int _money;

    public event Action<int> MoneyChanged;

    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            MoneyChanged?.Invoke(_money);
        }
    }

    public int MoneyMultiplier => _moneyMultiplier;

    public void IncreaseMoneyMultiplier(int step)
    {
        _moneyMultiplier += step;
    }
}