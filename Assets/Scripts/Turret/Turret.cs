using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Turret : MonoBehaviour, IAttackeble
{
    public static List<Turret> s_Turrets = new List<Turret>();

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
    
    [SerializeField] protected LayerMask triggerEnemy;
    [SerializeField] protected LayerMask triggerPlayer;

    [SerializeField] protected float maxHealth;
    [SerializeField] protected float curHealth;

    [SerializeField] protected TextMeshProUGUI textHealth;

    
    [SerializeField] protected float maximimTimeDismantling = 1;
    protected float currentTimeDismantling = 0;
    protected bool isDownKey = false;
    
    protected void Checking()
    {
        hitCollidersTargets = Physics2D.OverlapCircleAll(transform.position, radiusTargets, triggerEnemy);
        hitCollidersPlayer = Physics2D.OverlapCircleAll(transform.position, radiusPlayer, triggerPlayer);
        bool player = false;
        player = Action(player);

        if(!player && textPowerTime.gameObject.activeSelf)
        {
            textPowerTime.gameObject.SetActive(false);
        }

        if(isPower)
        {
            curPowerTime -= Time.deltaTime;
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
    }
    public virtual bool Action(bool player)
    {
        player = false;
        foreach (var item in hitCollidersPlayer)
        {
            if(item.CompareTag("Player"))
            {
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
                        textPowerTime.gameObject.SetActive(isPower);
                        indicatorActive.SetActive(isPower);
                    }
                    
                }

                if(isDownKey)
                {
                    if(currentTimeDismantling > 0)
                        currentTimeDismantling -= Time.deltaTime;
                    
                    // else
                    //     Dismantling();
                }

                    
                player = true;
                textPowerTime.gameObject.SetActive(true);
                textPowerTime.text = ((int)curPowerTime).ToString();
            }
        }

        return player;
    }
    
    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusTargets);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radiusPlayer);
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
