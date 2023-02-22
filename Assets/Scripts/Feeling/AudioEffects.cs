using UnityEngine;

namespace Feeling
{
    public class AudioEffects : MonoBehaviour
    {
        public static AudioEffects Instance { get; private set; }
        
        [SerializeField] private AudioClip[] _audioSteps;
        public AudioClip[] AudioSteps => _audioSteps;
        
        [SerializeField] private AudioClip _audioLvlUp;
        [SerializeField] private AudioClip _audioHealting;
        [SerializeField] private AudioClip _audioDamageToPlayer;
        [SerializeField] private AudioClip _audioMining;
        [SerializeField] private AudioClip _audioMiningFinaly;
        [SerializeField] private AudioClip _audioBuy;
        [SerializeField] private AudioClip _audioStep;
        [SerializeField] private AudioClip _audioTakeDrop;
        [SerializeField] private AudioClip _audioReload;
        [SerializeField] private AudioClip _audioFire;

        [SerializeField] private AudioClip _audioBtn1;

        [SerializeField] private AudioClip _audioMiningTurret;
        [SerializeField] private AudioClip _audioFireTurret;
        [SerializeField] private AudioClip _audioReloadTurret;

        [SerializeField] private AudioClip _audioBuild;
        [SerializeField] private AudioClip _audioBuildFinaly;
        [SerializeField] private AudioClip _audioBuildingLamp;
        [SerializeField] private AudioClip _audioBuildingShip;
        [SerializeField] private AudioClip _audioBuildingMineTurret;
        [SerializeField] private AudioClip _audioBuildingAttackTurret;
        


        public AudioClip AudioLvlUp => _audioLvlUp;
        public AudioClip AudioHealting => _audioHealting;
        public AudioClip AudioDamageToPlayer => _audioDamageToPlayer;
        public AudioClip AudioMining => _audioMining;
        public AudioClip AudioMininFinaly => _audioMiningFinaly;
        public AudioClip AudioBuy => _audioBuy;
        public AudioClip AudioStep => _audioStep;
        public AudioClip AudioTakeDrop => _audioTakeDrop;
        public AudioClip AudioReload => _audioReload;
        public AudioClip AudioFire => _audioFire;
        public AudioClip AudioBtn1 => _audioBtn1;
        
        public AudioClip AudioMiningTurret => _audioMiningTurret;
        public AudioClip AudioFireTurret => _audioFireTurret;
        public AudioClip AudioReloadTurret => _audioReloadTurret;

        public AudioClip AudioBuldingLamp => _audioBuildingLamp;
        public AudioClip AudioBuldingShip => _audioBuildingShip;
        public AudioClip AudioBuldingMineTurret => _audioBuildingMineTurret;
        public AudioClip AudioBuldingAttackTurret => _audioBuildingAttackTurret;
        public AudioClip AudioBuild => _audioBuild;
        public AudioClip AudioBuildFinaly => _audioBuildFinaly;


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