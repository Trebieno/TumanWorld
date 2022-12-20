using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Turret : MonoBehaviour
{
    [SerializeField] protected float radiusTargets;
    [SerializeField] protected float radiusPlayer;

    [SerializeField] protected Collider2D[] hitCollidersTargets;
    [SerializeField] protected Collider2D[] hitCollidersPlayer;

    [SerializeField] protected float maxPowerTime = 120;
    [SerializeField] protected float curPowerTime = 120;

    [SerializeField] protected TextMeshProUGUI textPowerTime;
    [SerializeField] protected GameObject indicatorActive;

    [SerializeField] protected Player player;
    [SerializeField] protected bool isPower;
    
    [SerializeField] protected LayerMask triggerEnemy;
    [SerializeField] protected LayerMask triggerPlayer;

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
                    isPower = !isPower;
                    textPowerTime.gameObject.SetActive(isPower);
                    indicatorActive.SetActive(isPower);
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
}
