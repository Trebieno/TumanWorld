﻿using System;
using UnityEngine;
using TMPro;

public class UpgradablePerks : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private SkillsUI _skillUI;

    [SerializeField] private Player _player;
    
    
    [SerializeField] private float _maxHealthIncreaseStep = 5;
    [SerializeField] private float _speedIncreaseStep = 1;
    [SerializeField] private float _damageIncreaseStep = 3;
    [SerializeField] private float _moneyIncreaseStep = 10;
    [SerializeField] private float _expirienceIncreaseStep = 2;
    [SerializeField] private float _fireSpeedIncreaseStep = 3;
    [SerializeField] private int _ammoIncreaseStep = 5;

    [SerializeField] private TMP_Text _textMainScreenScore;
    public event Action<int> ScoreChanged;

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            ScoreChanged?.Invoke(_score);
        }
    }

    private void Awake()
    {
        _player.Leveling.LevelChanged += Leveling_OnLevelChanged;
    }

    private void OnDestroy()
    {
        _player.Leveling.LevelChanged -= Leveling_OnLevelChanged;
    }

    private void Leveling_OnLevelChanged(int obj)
    {        
        Score += 3;
        _textMainScreenScore.gameObject.SetActive(true);
        _textMainScreenScore.text = $"Очки: {Score.ToString()}";
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
}

public enum Perks
{
    Health,
    Speed,
    Damage,
    Money,
    Expirience,
    FireSpeed,
    Ammo
}