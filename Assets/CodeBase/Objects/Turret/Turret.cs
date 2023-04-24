using UnityEngine;
using TMPro;
using System;

public class Turret : ObjectGame, IAttackeble
{
    [SerializeField] protected float radiusTargets;
    [SerializeField] protected float radiusPlayer;

    protected Collider2D[] hitCollidersTargets;
    protected Collider2D[] hitCollidersPlayer;
    
    [SerializeField] protected float maxPowerTime = 120;
    [SerializeField] protected float curPowerTime = 120;

    [SerializeField] protected TextMeshProUGUI textPowerTime;
    [SerializeField] protected GameObject indicatorActive;

    [SerializeField] protected bool isPower;
    
    [SerializeField] protected LayerMask enemyMask;

    [SerializeField] protected float maxHealth;
    [SerializeField] protected float curHealth;

    [SerializeField] protected TextMeshProUGUI textHealth;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected float damage;
    [SerializeField] protected float curDelayShoot;
    [SerializeField] protected float maxDelayShoot;
    [SerializeField] protected int score;
    [SerializeField] protected int maxAmmo = 30;
    [SerializeField] protected int curAmmo = 30;
    [SerializeField] private int _level;

    protected event Action onStart; 
    protected event Action onUpdate;
    protected event Action onUse;

    [Header("Upgratable")]
    [SerializeField] private float _maxHealthIncreaseStep = 5;
    [SerializeField] private float _speedIncreaseStep = 1;
    [SerializeField] private float _damageIncreaseStep = 3;
    [SerializeField] private float _fireSpeedIncreaseStep = 3;
    [SerializeField] private int _ammoIncreaseStep = 2;
    [SerializeField] private int _powerIncreaseStep = 10;
    [SerializeField] private float _radiusIncreaseStep = 5;

    [SerializeField] private int _oreCell;
    [SerializeField] private float _maxExp;
    [SerializeField] private float _curExp;
    public float MaxExpirience => _maxExp;

    public float CurrentExpirience
    {
        get => _curExp;
        private set
        {
            _curExp = value;
        }
    }

    public float Damage => damage;
    public float RotationSpeed => rotationSpeed;
    public float MaxHealth => maxHealth;
    public float CurHealth => curHealth;
    public int MaxAmmo => maxAmmo;
    public int CurAmmo => curAmmo;
    public int Level => _level;
    public int Score => score;
    public float Power => curPowerTime;
    public float RadiusTargets => radiusTargets;
    public int OreCell => _oreCell;

    private UpdateTurretMenu _updateTurretMenu;

    private void Start()
    {
        textPowerTime.gameObject.SetActive(false);
        textHealth.gameObject.SetActive(false);
        TurretsAll.Instance.Turrets.Add(this);
        Objects.Instance.ObjectsGame.Add(this);
        onStart.Invoke();


        _updateTurretMenu = PlayerCash.Instance.Player.UpgradableTurretPerks;
    }

    private void Update()
    {
        hitCollidersTargets = Physics2D.OverlapCircleAll(transform.position, radiusTargets, enemyMask);

        if(!player && textPowerTime.gameObject.activeSelf)
        {
            textPowerTime.gameObject.SetActive(false);
        }

        if(isPower)
        {
            curPowerTime -= Time.deltaTime;
            textPowerTime.text = ((int)curPowerTime).ToString();
        }

        if(curPowerTime <= 0 && isPower)
        {
            if(this.player.BattoryCount > 0)
            {
                this.player.BattoryCount -= 1;
                this.player.UpdateBattory();
                curPowerTime = maxPowerTime;
            }                        
            else
            {
                isPower = false;
                indicatorActive.SetActive(isPower);
            }                        
        }

        onUpdate.Invoke();
    }
    
    public override void Active()
    {
        if(!textPowerTime.gameObject.activeSelf)
        {
            textPowerTime.gameObject.SetActive(true);
            textHealth.gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {      
            isDownKey = true;         
            currentTimeDismantling = 0;
        }

        else if (Input.GetKeyUp(KeyCode.Z))
        {
            isDownKey = false;
            player.DestroySlider.gameObject.SetActive(false);
            if (currentTimeDismantling < maximimTimeDismantling)
            {
                isPower = !isPower;                                
                indicatorActive.SetActive(isPower);                
            }            
        }

        if(isDownKey)
        {
            if (currentTimeDismantling < maximimTimeDismantling)
            {
                if (!player.DestroySlider.gameObject.activeSelf)
                    player.DestroySlider.gameObject.SetActive(true);
                currentTimeDismantling += Time.deltaTime;
                player.DestroySlider.maxValue = maximimTimeDismantling;
                player.DestroySlider.value = currentTimeDismantling;


            }

            else
            {
                if (player.DestroySlider.gameObject.activeSelf)
                    player.DestroySlider.gameObject.SetActive(false);
                Dismantling(typeObject);
            }
        }       
    }

    public override void NotActive()
    {
        if(textPowerTime.gameObject.activeSelf)
        {
            textPowerTime.gameObject.SetActive(false);
            textHealth.gameObject.SetActive(false);
            currentTimeDismantling = 0;
            isDownKey = false;
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusTargets);
    }

    public void UpdateUIHealth()
    {
        textHealth.text = Math.Round(curHealth, 0) + "/" + Math.Round(maxHealth, 0);
    }

    public void SetDamage(float damage, Turret turret)
    {
        curHealth -= damage;
        UpdateUIHealth();
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void UpgradeSkills(int typeId)
    {
        Perks type = (Perks)typeId;
        if (score <= 0 && _oreCell > PlayerCash.Instance.Player.Mining.CurrentOre) return;

        switch (type)
        {
            case Perks.Health:                
                maxHealth += (maxHealth / 100) * _maxHealthIncreaseStep;
                curHealth = maxHealth;
                break;

            case Perks.Speed:
                rotationSpeed += (rotationSpeed / 100) * _speedIncreaseStep;
                break;

            case Perks.Damage:
                damage += (damage / 100) * _damageIncreaseStep;
                break;

            case Perks.FireSpeed:
                maxDelayShoot += (maxDelayShoot / 100) * _fireSpeedIncreaseStep;
                break;

            case Perks.Ammo:
                maxAmmo += _ammoIncreaseStep;                
                break;

            case Perks.Radius:
                radiusTargets += (radiusTargets / 100) * _radiusIncreaseStep;
                break;
            
            case Perks.Time:
                maxPowerTime += (maxPowerTime / 100) * _powerIncreaseStep;
                curPowerTime = maxPowerTime;
                break;
        }
        if(score > 0)
            score -= 1;
        else
        {
            PlayerCash.Instance.Player.Mining.CurrentOre -= _oreCell;
            _oreCell += (_oreCell / 100) * 10;
        }
        UpdateUIHealth();
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
        score += 1;
    }
}

