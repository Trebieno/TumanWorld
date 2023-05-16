using System;
using UnityEngine;
using TMPro;

public class UpgradablePerks : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private SkillsUI _skillUI;
    
    [SerializeField] private Player _player;

    private float _startingHealth;
    private float _startingSpeed;
    private float _startingDamage;
    private float _startingMoney;
    private float _startingExp;
    private float _startingFireSpeed;
    private int _startingAmmo;
    
    
    [SerializeField] private float _maxHealthIncreaseStep = 5;
    [SerializeField] private float _speedIncreaseStep = 1;
    [SerializeField] private float _damageIncreaseStep = 3;
    [SerializeField] private float _moneyIncreaseStep = 10;
    [SerializeField] private float _expirienceIncreaseStep = 2;
    [SerializeField] private float _fireSpeedIncreaseStep = 3;
    [SerializeField] private int _ammoIncreaseStep = 2;

    public event Action<int> ScoreChanged;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            ScoreChanged?.Invoke(_score);
        }
    }

    private void Start()
    {
        _player.Leveling.LevelChanged += Leveling_OnLevelChanged;

        _startingHealth = _player.Health.CurrentHealth;
        _startingSpeed = _player.Movement.MoveSpeed;
        _startingDamage = _player.Shooting.BulletDamage;
        _startingMoney = _player.Economic.MoneyMultiplier;
        _startingExp = _player.Leveling.ExpirienceMultiplier;
        _startingFireSpeed = _player.Shooting.DelayShoot;
        _startingAmmo = _player.Shooting.CurrentAmmo;
    }

    private void OnDestroy()
    {
        _player.Leveling.LevelChanged -= Leveling_OnLevelChanged;
    }

    private void Leveling_OnLevelChanged(int obj)
    {        
        Score += 3;
    }

    public void UpgradeSkills(int typeId)
    {
        Perks type = (Perks)typeId;
        if (_score <= 0) return;

        switch (type)
        {
            case Perks.Health:
                _player.Health.IncreaseMaxHealth(_maxHealthIncreaseStep);
                break;
            case Perks.Speed:
                _player.Movement.AddSpeedMovement(_speedIncreaseStep);
                break;
            case Perks.Damage:
                _player.Shooting.AddBulletDamage(_damageIncreaseStep);
                break;
            case Perks.Money:
                _player.Economic.IncreaseMoneyMultiplier(_moneyIncreaseStep);
                break;
            case Perks.Expirience:
                _player.Leveling.IncreaseExpirienceMultiplier(_expirienceIncreaseStep);
                break;
            case Perks.FireSpeed:
                _player.Shooting.AddSpeedFire(_fireSpeedIncreaseStep);
                break;
            case Perks.Ammo:
                _player.Shooting.AddAmmo(_ammoIncreaseStep);
                break;
        }

        _skillUI.UpdateSkill(type);
        Score -= 1;
    }

    public void ResetSkills()
    {
        _player.Health.SetHealth(_startingHealth);
        _player.Movement.SetSpeed(_startingSpeed);
        _player.Shooting.SetDamage(_startingDamage);
        _player.Economic.SetMultiplier(_startingMoney);
        _player.Leveling.SetExpirience(_startingExp);
        _player.Shooting.SetDelayShoot(_startingFireSpeed);
        _player.Shooting.SetAmmo(_startingAmmo);

        Score += _skillUI.ResetSkill();
        _player.UpdateUI();
    }
}


public enum Perks
{
    Health,
    Speed,
    Damage,
    Money,
    Expirience,
    FireSpeed,
    Ammo,
    Radius,
    Time
}