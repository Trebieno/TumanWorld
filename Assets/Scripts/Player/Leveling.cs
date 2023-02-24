using System;
using Feeling;
using UnityEngine;

public class Leveling : MonoBehaviour
{
    [SerializeField] private float _maxExpirience;
    [SerializeField] private float _curExp;
    
    [SerializeField] private int _level;
    [SerializeField] private float _expMultiplier;
    
    [SerializeField] private AudioSource _audioLvlUp;

    public event Action<int> LevelChanged; 
    public event Action<float, float> ExpirienceChanged; 

    public int Level => _level;

    public float MaxExpirience => _maxExpirience;

    public float CurrentExpirience
    {
        get => _curExp;
        private set
        {
            _curExp = value;
            ExpirienceChanged?.Invoke(_curExp, _maxExpirience);
        }
    }

    public float ExpirienceMultiplier => _expMultiplier;

    private void Start()
    {
        _audioLvlUp.clip = AudioEffects.Instance.AudioLvlUp;
    }

    public void AddExp(float exp) => CurrentExpirience += exp;
    
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
        
        _audioLvlUp.Play();
        
        //Instantiate(_particleLvlUp, transform.position, transform.rotation);
    }

    public void IncreaseExpirienceMultiplier(float step)
    {
        _expMultiplier += (_expMultiplier / 100) * step;
    }
}