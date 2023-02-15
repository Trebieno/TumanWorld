using System;
using UnityEngine;

public class UpgradablePerks : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private SkillsUI _skillUI;
    [Space]
    [SerializeField] private Health _health;
    [SerializeField] private Movement _movement;
    [SerializeField] private Shooting _shooting;
    [SerializeField] private Economic _economic;
    [SerializeField] private Leveling _leveling;
    [Space]
    [SerializeField] private int _maxHealthIncreaseStep = 10;
    [SerializeField] private float _speedIncreaseStep = 0.01f;
    [SerializeField] private float _damageIncreaseStep = 0.3f;
    [SerializeField] private int _moneyIncreaseStep = 1;
    [SerializeField] private int _expirienceIncreaseStep = 1;
    [SerializeField] private float _fireSpeedIncreaseStep = 0.003f;
    [SerializeField] private int _ammoIncreaseStep = 1;

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
        _leveling.LevelChanged += Leveling_OnLevelChanged;
    }

    private void OnDestroy()
    {
        _leveling.LevelChanged -= Leveling_OnLevelChanged;
    }

    private void Leveling_OnLevelChanged(int obj)
    {
        Score += 3;
    }

    public void UpgradeSkills(Perks type)
    {
        if (_score <= 0) return;

        switch (type)
        {
            case Perks.Health:
                _health.IncreaseMaxHealth(_maxHealthIncreaseStep);
                break;
            case Perks.Speed:
                _movement.AddSpeedMovement(_speedIncreaseStep);
                break;
            case Perks.Damage:
                _shooting.AddBulletDamage(_damageIncreaseStep);
                break;
            case Perks.Money:
                _economic.IncreaseMoneyMultiplier(_moneyIncreaseStep);
                break;
            case Perks.Expirience:
                _leveling.IncreaseExpirienceMultiplier(_expirienceIncreaseStep);
                break;
            case Perks.FireSpeed:
                _shooting.AddSpeedFire(_fireSpeedIncreaseStep);
                break;
            case Perks.Ammo:
                _shooting.AddAmmo(_ammoIncreaseStep);
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