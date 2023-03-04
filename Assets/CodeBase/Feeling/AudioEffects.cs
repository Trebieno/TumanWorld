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
        [SerializeField] private AudioClip _audioTakeDrop;
        [SerializeField] private AudioClip _audioReload;
        [SerializeField] private AudioClip _audioFire;

        [SerializeField] private AudioClip _audioMiningTurret;
        [SerializeField] private AudioClip _audioFireTurret;
        [SerializeField] private AudioClip _audioReloadTurret;

        [SerializeField] private AudioClip _audioBuild;
        [SerializeField] private AudioClip _audioBuildFinaly;
        [SerializeField] private AudioClip _audioBuildingLamp;
        [SerializeField] private AudioClip _audioBuildingShip;
        [SerializeField] private AudioClip _audioBuildingMineTurret;
        [SerializeField] private AudioClip _audioBuildingAttackTurret;

        [SerializeField] private AudioClip _audioHoverFx;
        [SerializeField] private AudioClip _audioClickFx;        

        [SerializeField] private AudioClip _audioTakingFx;
        [SerializeField] private AudioClip _audioLossFx;


        public AudioClip AudioLvlUp => _audioLvlUp;
        public AudioClip AudioHealting => _audioHealting;
        public AudioClip AudioDamageToPlayer => _audioDamageToPlayer;
        public AudioClip AudioMining => _audioMining;
        public AudioClip AudioMininFinaly => _audioMiningFinaly;
        public AudioClip AudioTakeDrop => _audioTakeDrop;
        public AudioClip AudioReload => _audioReload;
        public AudioClip AudioFire => _audioFire;
        
        public AudioClip AudioMiningTurret => _audioMiningTurret;
        public AudioClip AudioFireTurret => _audioFireTurret;
        public AudioClip AudioReloadTurret => _audioReloadTurret;

        public AudioClip AudioBuldingLamp => _audioBuildingLamp;
        public AudioClip AudioBuldingShip => _audioBuildingShip;
        public AudioClip AudioBuldingMineTurret => _audioBuildingMineTurret;
        public AudioClip AudioBuldingAttackTurret => _audioBuildingAttackTurret;
        public AudioClip AudioBuild => _audioBuild;
        public AudioClip AudioBuildFinaly => _audioBuildFinaly;

        public AudioClip AudioHoverFx => _audioHoverFx;
        public AudioClip AudioClickFx => _audioClickFx;

        public AudioClip AudioTakingFx => _audioTakingFx;
        public AudioClip AudioLossFx => _audioLossFx;


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