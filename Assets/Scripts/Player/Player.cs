using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IAttackeble
{
    [Header("Health")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _curHealth;
    
    [Header("Ore")]
    [SerializeField] private int _maxOre;
    [SerializeField] private int _curOre;
    [SerializeField] private float _timeMiningOre;
    [SerializeField] private TextMeshProUGUI _textOre;

    [Header("Level")]
    [SerializeField] private int _maxExp;
    [SerializeField] private int _curExp;
    
    [SerializeField] private int _level;
    [SerializeField] private TextMeshProUGUI _textLevel;

    [SerializeField] private int _score;
    [SerializeField] private TextMeshProUGUI _textScore;

    [Header("Up Multiplier")]
    [SerializeField] private int _moneyMultiplier;
    [SerializeField] private int _expMultiplier;
    [SerializeField] private int _miningMultiplier;

    [Header("Slider")]
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _expSlider;

    private Movement _movement;
    private Shooting _shooting;
    private SkillsUI _skillUI;

    public Movement Movement => _movement;


    [Header("Info Screen")]

    public int Money;
    [SerializeField] private TextMeshProUGUI _textMoney;
    [Space(5)]
    public int LightCount;

    [Space(5)]
    public int BattoryCount;
    [SerializeField] private TextMeshProUGUI _textBattory;

    [Space(5)]
    public int ShipCount;

    [Space(5)]
    public bool Flashlight;
    [SerializeField] private GameObject _imageFlaslight;

    [Space(5)]
    public bool Pickaxe;
    [SerializeField] private GameObject _imagePickaxe;

    [Space(5)]
    public int MineTurretCount;
    public int AttackTurretCount;

    [Space(5)]
    [SerializeField] private AudioSource _audioLvlUp;
    [SerializeField] private AudioSource _audioHealting;
    [SerializeField] private ParticleSystem _particleLvlUp;
    [SerializeField] private ScrollViewResourse _scrollViewResourse;

    public int GetLevel() => _level;
    public int GetMoneyMultiplier() => _moneyMultiplier;
    public int GetExpMultiplier() => _expMultiplier;
    public void AddExp(int exp) => _curExp += exp;
    public void UpdateAmmo() => _shooting.UpdateAmmo();
    
    public void UpdateMoney() => _textMoney.text = Money.ToString();

    public int GetCountOre() => _curOre;
    public int RemoveOre(int ore) => _curOre -= ore;
    public int GetMiningMultiplier() => _miningMultiplier;
    public void AddOre(int ore) => _curOre += ore;
    public float GetTimeMiningOre() => _timeMiningOre;
    public void UpdateTextOre() => _textOre.text = _curOre.ToString();
    public void UpdateScore() => _textScore.text = $"Очки навыков: {_score.ToString()}";
    public void UpdateBattory() => _textBattory.text = BattoryCount.ToString();
    public void UpdateScrollView()
    {
        if(_scrollViewResourse.Index == 0)
            _scrollViewResourse.TextInventory.text = ShipCount.ToString();
        if(_scrollViewResourse.Index == 1)
            _scrollViewResourse.TextInventory.text = LightCount.ToString();
        if(_scrollViewResourse.Index == 2)
            _scrollViewResourse.TextInventory.text = AttackTurretCount.ToString();
        if(_scrollViewResourse.Index == 3)
            _scrollViewResourse.TextInventory.text = MineTurretCount.ToString();
    }

    public float Healing()
    {
        _audioHealting.Play();
        _curHealth += 50;
        if(_curHealth > _maxHealth)
            _curHealth = _maxHealth;
        return 0;
        
    } 

    public int ActivateFlashLight()
    {
        Flashlight = true;
        _imageFlaslight.gameObject.SetActive(true);
        return 0;
    }

    public int ActivatePickaxe()
    {
        Pickaxe = true;
        _imagePickaxe.gameObject.SetActive(true);
        return 0;
    }

    public int AddClip(int clip)
    {
        _shooting.AddClip(clip);
        return 0;
    }

    private void Start() 
    {
        _textLevel.text =  $"Level {_level.ToString()}";
        _movement = GetComponent<Movement>();
        _shooting = GetComponent<Shooting>();
        _skillUI = GetComponent<SkillsUI>();

        _imageFlaslight.gameObject.SetActive(false);
        _imagePickaxe.gameObject.SetActive(false);

        
        UpdateUI();
    }

    public void UpdateUI()
    {
        UpdateSliders();
        UpdateMoney();
        UpdateTextOre();
        UpdateAmmo();
        UpdateScore();
        UpdateBattory();        
        _shooting.UpdateClip();        
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
        _score += 2;
        _textLevel.text =  $"Level {_level.ToString()}";
        UpdateScore();
        _audioLvlUp.Play();
        Instantiate(_particleLvlUp, transform.position, transform.rotation);
    }

    public void UpgradeSkills(string type) 
    {
        if(_score > 0)
        {
            if(type == "health") _maxHealth += 10;
            if(type == "speed") _movement.AddSpeedMovement(0.01f);
            if(type == "damage") _shooting.AddBulletDamage(0.3f);
            if(type == "money") _moneyMultiplier += 1;
            if(type == "exp") _expMultiplier += 1;
            if(type == "speedFire") _shooting.AddSpeedFire(0.003f);
            if(type == "ammo") _shooting.AddAmmo(1);
      

            _skillUI.UpdateSkill(type);
            _score -= 1;
            UpdateUI();
        }
        
        UpdateSliders();
    }

    public void SetDamage(int damage)
    {        
        _curHealth -= damage;
        UpdateSliders();
        if (_curHealth <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        Debug.Log(_curHealth +" "+ gameObject.name);
    }

    public void UpdateSliders()
    {
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _curHealth;
        _expSlider.maxValue = _maxExp;
        _expSlider.value = _curExp;
    }

    public void StateShooting(bool state) => _shooting.enabled = state;
}

