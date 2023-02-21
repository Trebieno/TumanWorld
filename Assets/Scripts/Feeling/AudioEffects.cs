using UnityEngine;

namespace Feeling
{
    public class AudioEffects : MonoBehaviour
    {
        public static AudioEffects Instance { get; private set; }

        [SerializeField] private AudioSource[] _audioSteps;
        public AudioSource[] AudioSteps => _audioSteps;
        
        [SerializeField] private AudioSource _audioLvlUp;
        [SerializeField] private AudioSource _audioHealting;
        [SerializeField] private AudioSource _audioDamageToPlayer;
        [SerializeField] private AudioSource _audioMining;
        [SerializeField] private AudioSource _audioMiningFinaly;
        [SerializeField] private AudioSource _audioBuy;
        [SerializeField] private AudioSource _audioStep;
        [SerializeField] private AudioSource _audioTakeDrop;
        [SerializeField] private AudioSource _audioReload;
        [SerializeField] private AudioSource _audioFire;

        [SerializeField] private AudioSource _audioBtn1;

        [SerializeField] private AudioSource _audioMiningTurret;
        [SerializeField] private AudioSource _audioFireTurret;
        [SerializeField] private AudioSource _audioReloadTurret;

        [SerializeField] private AudioSource _audioBuild;
        [SerializeField] private AudioSource _audioBuildFinaly;
        [SerializeField] private AudioSource _audioBuildingLamp;
        [SerializeField] private AudioSource _audioBuildingShip;
        [SerializeField] private AudioSource _audioBuildingMineTurret;
        [SerializeField] private AudioSource _audioBuildingAttackTurret;
        


        public AudioSource AudioLvlUp => _audioLvlUp;
        public AudioSource AudioHealting => _audioHealting;
        public AudioSource AudioDamageToPlayer => _audioDamageToPlayer;
        public AudioSource AudioMining => _audioMining;
        public AudioSource AudioMininFinaly => _audioMiningFinaly;
        public AudioSource AudioBuy => _audioBuy;
        public AudioSource AudioStep => _audioStep;
        public AudioSource AudioTakeDrop => _audioTakeDrop;
        public AudioSource AudioReload => _audioReload;
        public AudioSource AudioFire => _audioFire;
        public AudioSource AudioBtn1 => _audioBtn1;
        
        public AudioSource AudioMiningTurret => _audioMiningTurret;
        public AudioSource AudioFireTurret => _audioFireTurret;
        public AudioSource AudioReloadTurret => _audioReloadTurret;

        public AudioSource AudioBuldingLamp => _audioBuildingLamp;
        public AudioSource AudioBuldingShip => _audioBuildingShip;
        public AudioSource AudioBuldingMineTurret => _audioBuildingMineTurret;
        public AudioSource AudioBuldingAttackTurret => _audioBuildingAttackTurret;
        public AudioSource AudioBuild => _audioBuild;
        public AudioSource AudioBuildFinaly => _audioBuildFinaly;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


    }
}