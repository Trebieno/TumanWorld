using System;
using Feeling;
using UnityEngine;

public class Leveling : MonoBehaviour
{
    [SerializeField] private float _maxExp;
    [SerializeField] private float _curExp;
    
    [SerializeField] private int _level;
    [SerializeField] private float _expMultiplier;
    
    [SerializeField] private AudioSource _audioLvlUp;

    public event Action<int> LevelChanged; 
    public event Action<float, float> ExpirienceChanged;

    [SerializeField] private GameObject _imageNewLvl;

    public int Level => _level;

    public GameObject ImageNewLvl => _imageNewLvl;

    public float MaxExpirience => _maxExp;

    public float CurrentExpirience
    {
        get => _curExp;
         set
        {
            _curExp = value;
            ExpirienceChanged?.Invoke(_curExp, _maxExp);
        }
    }

    public float ExpirienceMultiplier => _expMultiplier;

    private void Start()
    {
        _audioLvlUp.clip = AudioEffects.Instance.AudioLvlUp;
    }

    public void AddExp(float exp)
    {
        CurrentExpirience += exp;
        CheckUpdateLevel();
    }

    public void CheckUpdateLevel()
    {
        if(_curExp >=_maxExp)
            UpgradeLevel();
    }
    
    private void UpgradeLevel()
    {
        _level += 1;
        _maxExp += (_maxExp * 10) / 100;
        _curExp = 0;
        
        LevelChanged?.Invoke(_level);
        ExpirienceChanged?.Invoke(_curExp, _maxExp);
        _audioLvlUp.Play();
        _imageNewLvl.SetActive(true);
        //Instantiate(_particleLvlUp, transform.position, transform.rotation);
    }

    public void IncreaseExpirienceMultiplier(float step)
    {
        _expMultiplier += (_expMultiplier / 100) * step;
    }

    public void SetExpirience(float step)
    {
        _expMultiplier = step;
    }
}