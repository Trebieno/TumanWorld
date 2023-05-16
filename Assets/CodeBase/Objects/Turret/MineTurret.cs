using UnityEngine;
using Feeling;

public class MineTurret : Turret
{
    [SerializeField] private AudioSource _audioMiningTurret;
    [SerializeField] private GameObject _bur;

    private void Awake()
    {
        damage = Time.deltaTime;
        onStart += OnStart;
        onUpdate += OnUpdate;
        typeObject = GameObjects.MiningTurret;
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

                ore.MineTurret(this);
                _bur.transform.Rotate(0, 0, rotationSpeed);
            }
        }

        if(!isPower && _audioMiningTurret.isPlaying)
            _audioMiningTurret.Stop();
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
                textPowerTime.gameObject.SetActive(isPower);
                indicatorActive.SetActive(isPower);
                
                if(!isPower)
                    _audioMiningTurret.Stop();
            }
            
        }

        if(isDownKey)
        {
            if (currentTimeDismantling < maximimTimeDismantling)
            {
                if (!player.DestroySlider.gameObject.activeSelf)
                    player.DestroySlider.gameObject.SetActive(true);
                currentTimeDismantling += damage;
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
}
