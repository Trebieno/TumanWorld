using UnityEngine;

namespace Feeling
{
    public class AudioEffects : MonoBehaviour
    {
        public static AudioEffects Instance { get; private set; }

        [Space(5)] [SerializeField] private AudioSource _audioLvlUp;
        [SerializeField] private AudioSource _audioHealting;

        public AudioSource AudioLvlUp => _audioLvlUp;

        public AudioSource AudioHealting => _audioHealting;

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