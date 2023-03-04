using UnityEngine;
using TMPro;
using System;

public class Turret : MonoBehaviour, IAttackeble
{
    [SerializeField] private Turrets turret;
    [SerializeField] protected float radiusTargets;
    [SerializeField] protected float radiusPlayer;

    protected Collider2D[] hitCollidersTargets;
    protected Collider2D[] hitCollidersPlayer;
    
    [SerializeField] protected float maxPowerTime = 120;
    [SerializeField] protected float curPowerTime = 120;

    [SerializeField] protected TextMeshProUGUI textPowerTime;
    [SerializeField] protected GameObject indicatorActive;

    [SerializeField] protected Player player;
    [SerializeField] protected bool isPower;
    
    [SerializeField] protected LayerMask enemyMask;

    [SerializeField] protected float maxHealth;
    [SerializeField] protected float curHealth;

    [SerializeField] protected TextMeshProUGUI textHealth;

    
    [SerializeField] protected float maximimTimeDismantling = 1;
    protected float currentTimeDismantling = 0;
    protected bool isDownKey = false;
    
    protected event Action onStart; 
    protected event Action onUpdate;
    protected event Action onUse;

    private void Start()
    {
        textPowerTime.gameObject.SetActive(false);
        textHealth.gameObject.SetActive(false);

        TurretsAll.Instance.Turrets.Add(this);
        onStart.Invoke();
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
    
    public virtual void Active()
    {
        if(!textPowerTime.gameObject.activeSelf)
        {
            textPowerTime.gameObject.SetActive(true);
            textHealth.gameObject.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {      
            isDownKey = true;         
            currentTimeDismantling = maximimTimeDismantling;
        }

        else if (Input.GetKeyUp(KeyCode.Z))
        {
            isDownKey = false;
            if(currentTimeDismantling > 0)
            {
                isPower = !isPower;                                
                indicatorActive.SetActive(isPower);                
            }            
        }

        if(isDownKey)
        {
            if(currentTimeDismantling > 0)
                currentTimeDismantling -= Time.deltaTime;
            
            else
                Dismantling(turret);
        }       
    }

    public void NotActive()
    {
        if(textPowerTime.gameObject.activeSelf)
        {
            textPowerTime.gameObject.SetActive(false);
            textHealth.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusTargets);
    }

    public void Dismantling(Turrets turret)
    {
        switch (turret)
        {
            case Turrets.Mining:
            player.MineTurretCount += 1;
            break;

            case Turrets.Attack:
            player.AttackTurretCount += 1;
            break;            
        }

        player.UpdateUI();
        Destroy(gameObject);

    }

    public void UpdateUIHealth()
    {
        textHealth.text = curHealth.ToString() + "/" + maxHealth;
    }

    public void SetDamage(float damage)
    {
        curHealth -= damage;
        UpdateUIHealth();
        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

public enum Turrets
{
    Mining,
    Attack,
}
