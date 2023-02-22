using UnityEngine;
using Feeling;

public class MineTurret : Turret
{
    [SerializeField] private AudioSource _audioMiningTurret;
    private Turrets turret = Turrets.Mining;

    private void Start() 
    {

        _audioMiningTurret.clip = AudioEffects.Instance.AudioMiningTurret;

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
                if(!_audioMiningTurret.isPlaying)
                    _audioMiningTurret.Play();

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
                        
                        if(!isPower)
                            _audioMiningTurret.Stop();
                    }
                    
                }

                if(isDownKey)
                {
                    if(currentTimeDismantling > 0)
                        currentTimeDismantling -= Time.deltaTime;
                    
                    else
                        Dismantling(turret);
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
