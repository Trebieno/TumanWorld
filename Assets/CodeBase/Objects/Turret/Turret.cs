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

    protected event Action onStart; 
    protected event Action onUpdate;
    protected event Action onUse;

    private void Start()
    {
        textPowerTime.gameObject.SetActive(false);
        textHealth.gameObject.SetActive(false);
        Objects.Instance.ObjectsGame.Add(this);
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

