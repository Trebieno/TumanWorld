using UnityEngine;
using Feeling;

public class MineTurret : Turret
{
    private void Start() 
    {
        isPower = false;
        indicatorActive.SetActive(isPower);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        s_Turrets.Add(this);
        UpdateUIHealth();
    }

    public override bool Action(bool player)
    {
        foreach (var item in hitCollidersTargets)
        {
            Ore ore = item.GetComponent<Ore>();
            if(ore != null && isPower)
            {
                if(!AudioEffects.Instance.AudioMiningTurret.isPlaying)
                    AudioEffects.Instance.AudioMiningTurret.Play();

                ore.MineTurret();
            }
        }
        
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
                    if(!isPower)
                        AudioEffects.Instance.AudioMiningTurret.Stop();
                }
                    
                player = true;
                textPowerTime.gameObject.SetActive(true);
                textPowerTime.text = ((int)curPowerTime).ToString();
            }
        }

        return player;
    }
    private void Update() 
    {
        Checking();           
    }
}
