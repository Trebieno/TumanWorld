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

    [Header("Limits")]
    [SerializeField] private int _limitNpc = 150;
    [SerializeField] private int _limitSpawn = 10;

    public ObjectGame[] possibleTargets; // список всех возможных целей
    public float attackRange; // дистанция атаки
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
            SpawnNpc(GetAttackTarget());

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


        // получаем список всех возможных целей
        possibleTargets = Objects.Instance.ObjectsGame.ToArray();

        // ищем цель с наибольшим весом
        float maxWeight = 0;
        ObjectGame target = null;

        foreach (ObjectGame possibleTarget in possibleTargets) 
        {
            float weight = CalculateWeight(possibleTarget);

            if (weight > maxWeight) 
            {
                maxWeight = weight;
                if(Random.Range(1, 100) <= 30)
                    target = possibleTarget;
                else
                    target = PlayerCash.Instance.Player.transform;
            }
        }

        // если есть цель, находящаяся в дистанции атаки, атакуем ее
        // if (target != null && Vector3.Distance(transform.position, target.transform.position) < attackRange) {
        //     Attack(target);
        // }
    }

    private void Attack(ObjectGame target) 
    {
        
    }

    private float CalculateWeight(ObjectGame possibleTarget) 
    {
        float weight = 0;

        // оцениваем параметры цели и присваиваем ей вес
        float distance = Vector3.Distance(transform.position, possibleTarget.transform.position);
        weight += (attackRange - distance) / attackRange;
        
        float health = possibleTarget.CurHealth;
        weight += health / 100;

        // можно добавить другие параметры, например тип или количество защиты

        return weight;
    }

    private Character GetAttackTarget()
    {
        float closestDistance = Mathf.Infinity;
        Character closestTarget = null;

        foreach (var enemy in EnemyAll.Instance.Characters)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestTarget = enemy;
            }
        }

        return closestTarget;
    }

    private void Reproduction()
    {
        _curTimeSpawnReproduction = Random.Range(_maxTimeSpawnReproduction/2, _maxTimeSpawnReproduction*2);
        SpawnPoints.Instance.SpawnPointsList.RemoveAll(x => x == null);
        if(SpawnPoints.Instance.SpawnPointsList.Count >= _limitSpawn)
            return;

        Vector3 centrPoint = transform.position;
        Vector3 randomPoint = centrPoint + new Vector3(Random.value-0.5f,Random.value-0.5f,Random.value-0.5f).normalized * _radiusReproduction;

        Instantiate(_spawnPrefab, randomPoint, Quaternion.identity).Reset();
        _curExp += 5;
        if(_curExp >= _maxExp)
            UpgradeLevel();
    }

    private void SpawnNpc(Character target)
    {
        _curTimeSpawnNpc = Random.Range(_maxTimeSpawnNpc/2, _maxTimeSpawnNpc*2);
        EnemyAll.Instance.Characters.RemoveAll(x => x == null);
        if(EnemyAll.Instance.Characters.Count >= _limitNpc)
            return;

        Vector3 centrPoint = transform.position;
        Vector3 randomPoint = centrPoint + new Vector3(Random.value-0.5f,Random.value-0.5f,Random.value-0.5f).normalized * _radiusSpawn;
        Vector3 spawnPoint = target != null ? randomPoint : transform.position;

        var npc = Instantiate(_npcPrefub, spawnPoint, Quaternion.identity);
        npc.InitializeSpecifications(_level);
        npc.SetAttackTarget(PlayerCash.Instance.Player.transform);
        _curExp += 1;
        if(_curExp >= _maxExp)
            UpgradeLevel();
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
        _maxExp += (_maxExp * 40) / 100;
        _curExp = 0;
        _maxTimeSpawnNpc -= (_maxTimeSpawnNpc / 100) * 20;

        // _audioLvlUp.Play();
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

                Vector3 centrPoint = transform.position;
                Vector3 randomPoint = centrPoint + new Vector3(Random.value-0.5f,Random.value-0.5f,Random.value-0.5f).normalized * 0.5f;
                Instantiate(lootObjects[randomIndex], randomPoint, new Quaternion(0, 0, Random.Range(0, 355), 0));
            }
        }
    }

    public void Reset()
    {
        _curExp = 0;
        _maxExp = 20;
        _maxHealth = 100;
        _curHealth = _maxHealth;
        _level = 1;
        _maxTimeSpawnNpc = 20;
        _curTimeSpawnNpc = _maxTimeSpawnNpc;
    }

    public void SetDamage(float damage, Turret turret = null)
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
