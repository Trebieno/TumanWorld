using UnityEngine;
using Feeling;

public class MineTurret : Turret
{
    [SerializeField] private AudioSource _audioMiningTurret;
    private Turrets turret = Turrets.Mining;

    private void Awake()
    {
        onStart += OnStart;
        onUpdate += OnUpdate;
    }

    private void OnStart() 
    {

        _audioMiningTurret.clip = AudioEffects.Instance.AudioMiningTurret;

        isPower = false;
        indicatorActive.SetActive(isPower);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        UpdateUIHealth();
    }

    private void OnUpdate()
    {
        foreach (var item in hitCollidersTargets)
        {
            Ore ore = OreAll.Instance.Ores.Find(x => x.transform == item.transform);
            if(ore != null && isPower)
            {
                if(!_audioMiningTurret.isPlaying)
                    _audioMiningTurret.Play();

                ore.MineTurret();
            }
        }
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
    }
}
