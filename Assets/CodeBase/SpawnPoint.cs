using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class SpawnPoint : MonoBehaviour, IAttackeble
{
    [SerializeField] private Character _npcPrefub;
    [SerializeField] private SpawnPoint _spawnPrefab;
    [SerializeField] private SpriteRenderer _skin;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private List<GameObject> lootObjects;
    [SerializeField] private AudioSource _audioLvlUp;


    [Header("Health")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _curHealth;

    [Header("Specifications")]
    [SerializeField] private int _level = 1;
    [SerializeField] private int _maxExp = 20;
    [SerializeField] private int _curExp = 0;
    [SerializeField] private int _countLoot => _level + 3;

    [Header("Time Reproduction")]
    [SerializeField] private float _maxTimeSpawnReproduction;
    [SerializeField] private float _curTimeSpawnReproduction;

    [Header("Time SpawnNpc")]
    [SerializeField] private float _maxTimeSpawnNpc;
    [SerializeField] private float _curTimeSpawnNpc;

    [Header("Time HideBar")]
    [SerializeField] private float _maxTimeHideBar;
    [SerializeField] private float _curTimeHideBar;

    [Header("Radius")]
    [SerializeField] private float _radiusSpawn;
    [SerializeField] private float _radiusReproduction;

    
    
    private void Start() 
    {
        _healthSlider.gameObject.SetActive(false);
        _curTimeSpawnReproduction = Random.Range(_maxTimeSpawnReproduction/2, _maxTimeSpawnReproduction*2);
        SpawnPoints.Instance.SpawnPointsList.Add(this);
    }

    private void Update() 
    {
        if(_curTimeSpawnNpc > 0)
            _curTimeSpawnNpc -= Time.deltaTime;
        else
            SpawnNpc();
        
        if(_level >= 5)
        {
            if(_curTimeSpawnReproduction > 0)
                    _curTimeSpawnReproduction -= Time.deltaTime;
            else
                Reproduction();
        }


        if(_curTimeHideBar > 0)
            _curTimeHideBar -=Time.deltaTime;
        else
            _healthSlider.gameObject.SetActive(false);
    }

    private void Reproduction()
    {
        
        Vector3 centrPoint = transform.position;
        Vector3 randomPoint = centrPoint + new Vector3(Random.value-0.5f,Random.value-0.5f,Random.value-0.5f).normalized * _radiusReproduction;

        Instantiate(_spawnPrefab, randomPoint, Quaternion.identity);
        _curTimeSpawnReproduction = Random.Range(_maxTimeSpawnReproduction/2, _maxTimeSpawnReproduction*2);
        _curExp += 5;
    }

    private void SpawnNpc()
    {
        Vector3 centrPoint = transform.position;
        Vector3 randomPoint = centrPoint + new Vector3(Random.value-0.5f,Random.value-0.5f,Random.value-0.5f).normalized * _radiusSpawn;

        var npc = Instantiate(_npcPrefub, randomPoint, Quaternion.identity);
        npc.InitializeSpecifications(_level);
        _curTimeSpawnNpc = Random.Range(_maxTimeSpawnNpc/2, _maxTimeSpawnNpc*2);
        _curExp += 1;
    }
    

    private void UpdateHealth() 
    {
            _healthSlider.maxValue = _maxHealth;
            _healthSlider.value = _curHealth;

            _curTimeHideBar = _maxTimeHideBar;
            _healthSlider.gameObject.SetActive(true);
    }

    private void UpgradeLevel()
    {
        _level += 1;
        _maxExp += (_maxExp * 10) / 100;
        _curExp = 0;

        _audioLvlUp.Play();
        //Instantiate(_particleLvlUp, transform.position, transform.rotation);
    }

    private void OnDrawGizmos() 
    {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.localPosition, _radiusSpawn);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.localPosition, _radiusReproduction);
    }
    private void SpawnLoot(int countLoot)
    {
        for (int i = 0; i < countLoot; i++)
        {
            if (Random.Range(0, 100) <= 50)
            {
                int randomIndex = Random.Range(0, lootObjects.Count);

                Instantiate(lootObjects[randomIndex], transform.position, transform.rotation);
            }
        }
    }
    public void SetDamage(float damage)
    {            
        _curHealth -= damage;
        if (_curHealth <= 0)
        {
            SpawnLoot(_countLoot);
            Destroy(gameObject); 
        }
        UpdateHealth();
    }
}
