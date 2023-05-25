using System;
using UnityEngine;

public class Mining : MonoBehaviour
{
    [SerializeField] private int _maxOre;
    [SerializeField] private int _curOre;
    [SerializeField] private float _timeMiningOre;
    [SerializeField] private int _miningMultiplier;

    public event Action<int> OreChanged;

    public int CurrentOre
    {
        get => _curOre;
        set
        {
            _curOre = value;            
            OreChanged?.Invoke(_curOre);
        }
    }
    public int RemoveOre(int ore) => CurrentOre -= ore;
    public void AddOre(int ore) => CurrentOre += ore;
    public float GetTimeMiningOre() => _timeMiningOre;
    public int GetMiningMultiplier() => _miningMultiplier;
    
    public void UpdateTime()
    {
        _timeMiningOre += (_timeMiningOre/100) * 50;
    }
}