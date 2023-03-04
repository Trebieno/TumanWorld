using UnityEngine;

namespace Feeling
{
    public class Particles : MonoBehaviour
    {
        public static Particles Instance { get; private set; }

        private ParticleSystem _particleEnemyDeath;

        public ParticleSystem ParticleEnemyDeath => _particleEnemyDeath;

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