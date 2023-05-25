using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Feeling;
using Pathfinding;

public class Character : MonoCache, IAttackeble
{
   
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float angleOffset = 10f;
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private int level;

    [SerializeField] protected float damage;
    [SerializeField] protected float maxDistanceToPlayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private GameObject _destructionParticlePrefab;
    [SerializeField] private AudioClip _deathSound;

    [Header("Culldown attack")] [SerializeField]
    private float _maxCulldown;

    [SerializeField] private float _curCulldown;

    [SerializeField] private int _exp;
    

    [SerializeField] private GameObject[] _lootPrefabs;
    [SerializeField] private float[] _lootChances;

    [SerializeField] private AudioClip[] _hurtSounds;
    [SerializeField] private float _spawnRadius = 0.5f;

    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private Transform _target;

    [SerializeField] private AIDestinationSetter _agent;
    [SerializeField] private AIPath _aiPath;
    private float _currentHealth;
    private AudioSource _audioSource;

    private void Start()
    {
        EnemyAll.Instance.Characters.Add(this);
        // _agent = GetComponent<AIDestinationSetter>();
        // _aiPath = GetComponent<AIPath>();
        // target = PlayerCash.Instance.Player.transform;
        _currentHealth = maxHealth;

        _audioSource = GetComponent<AudioSource>();
    }

    public override void OnTick()
    {
        if (_target != null)
        {
            _agent.target = _target;

            if (Vector3.Distance(_attackPoint.position, _target.position) <= maxDistanceToPlayer)
            {
                if (_curCulldown <= 0f)
                {
                    Attack();
                    _curCulldown = _maxCulldown;
                }
                else
                {
                    _curCulldown -= Time.deltaTime;
                }
            }
        }

        else
        {
            Objects.Instance.ObjectsGame.RemoveAll(x => x == null);
        
            // получаем список всех возможных целей
            ObjectGame[] possibleTargets = Objects.Instance.ObjectsGame.ToArray();


            // ищем цель с наибольшим весом
            float maxWeight = 0;            

            foreach (ObjectGame possibleTarget in possibleTargets) 
            {
                float weight = CalculateWeight(possibleTarget);

                if (weight > maxWeight) 
                {
                    maxWeight = weight;
                    if(Random.Range(1, 100) <= 20)
                        _target = possibleTarget.transform;
                }
            }

            if(_target == null)
                _target = PlayerCash.Instance.Player.transform;
        }
    }

    private float CalculateWeight(ObjectGame possibleTarget) 
    {
        float weight = 0;

        // оцениваем параметры цели и присваиваем ей вес
        float distance = Vector3.Distance(transform.position, possibleTarget.transform.position);
        weight += (1000 - distance) / 1000;
        
        float health = possibleTarget.CurHealth;
        weight += health / 100;

        // можно добавить другие параметры, например тип или количество защиты

        return weight;
    }
    
    public void InitializeSpecifications(int level)
    {        
        this.level = level;
        _exp = Random.Range(this.level, this.level + 5);
        maxHealth = Random.Range(10, this.level);
        _currentHealth = maxHealth;
        damage = Random.Range(1, this.level + 5);
        moveSpeed = Random.Range(1f, this.level / 30); // с 80 на 30
        if(_agent == null)
            _agent = GetComponent<AIDestinationSetter>();
        _aiPath.maxSpeed = moveSpeed;
    }

    public void SetAttackTarget(Transform target)
    {
        this._target = target;
    }

    private int maxHits = 5;
    private void Attack()
    {
        Collider2D[] hitColliders = new Collider2D[maxHits];
        int numColliders = Physics2D.OverlapCircleNonAlloc(transform.position, maxDistanceToPlayer, hitColliders, _targetMask);

        for (int i = 0; i < numColliders; i++)
        {
            IAttackeble attackable = hitColliders[i].GetComponent<IAttackeble>();
            if (attackable != null)
            {
                attackable.SetDamage(damage);
            }
        }
    }


    public void SetDamage(float damage, Turret turret = null)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();

            if(turret == null)
                PlayerCash.Instance.Player.Leveling.AddExp(_exp);
            else
                turret.AddExp(_exp);
        }
        else
        {
            int randomSoundIndex = Random.Range(0, _hurtSounds.Length-1);
            _audioSource.PlayOneShot(_hurtSounds[randomSoundIndex]);
        }
    }

    private void Die()
    {
    
        // Spawn loot if any available
        for (int i = 0; i < _lootPrefabs.Length; i++)
        {
            float randomChance = Random.Range(0f, 1f);
            if (randomChance <= _lootChances[i])
            {
                Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * _spawnRadius;
                Instantiate(_lootPrefabs[i], spawnPosition, Quaternion.identity);
            }
        }

        GameObject destructionEffect = Instantiate(_destructionParticlePrefab, transform.position, Quaternion.identity);

        _audioSource.PlayOneShot(_deathSound);
        EnemyAll.Instance.Characters.Remove(this);
        Destroy(gameObject);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, maxDistanceToPlayer);
    }
}