using System;
using Feeling;
using UnityEngine;

public class Leveling : MonoBehaviour
{
    [SerializeField] private int _maxExpirience;
    [SerializeField] private int _curExp;
    
    [SerializeField] private int _level;
    [SerializeField] private int _expMultiplier;

    public event Action<int> LevelChanged; 
    public event Action<int, int> ExpirienceChanged; 

    public int Level => _level;

    public int MaxExpirience => _maxExpirience;

    public int CurrentExpirience
    {
        get => _curExp;
        private set
        {
            _curExp = value;
            ExpirienceChanged?.Invoke(_curExp, _maxExpirience);
        }
    }

    public int ExpirienceMultiplier => _expMultiplier;
    public void AddExp(int exp) => CurrentExpirience += exp;
    
    public void CheckUpdateLevel()
    {
        if(_curExp >=_maxExpirience)
            UpgradeLevel();
    }
    
    private void UpgradeLevel()
    {
        _level += 1;
        _maxExpirience += (_maxExpirience * 10) / 100;
        _curExp = 0;
        
        LevelChanged?.Invoke(_level);
        
        AudioEffects.Instance.AudioLvlUp.Play();
        //Instantiate(_particleLvlUp, transform.position, transform.rotation);
    }

    public void IncreaseExpirienceMultiplier(int step)
    {
        _expMultiplier += step;
    }
}