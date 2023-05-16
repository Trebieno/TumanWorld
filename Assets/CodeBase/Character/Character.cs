using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Feeling;

public class Character : MonoBehaviour, IAttackeble
{
   
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float angleOffset = 10f;
    [SerializeField] private float rotationSpeed = 1000f;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private int level;

    [SerializeField] protected float damage;
    [SerializeField] protected float maxDistanceToPlayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject destructionParticlePrefab;
    [SerializeField] private AudioClip deathSound;

    [Header("Culldown attack")] [SerializeField]
    private float _maxCulldown;

    [SerializeField] private float _curCulldown;

    [SerializeField] private int _exp;
    

    [SerializeField] private GameObject[] lootPrefabs;
    [SerializeField] private float[] lootChances;

    [SerializeField] private AudioClip[] hurtSounds;
    [SerializeField] private float _spawnRadius = 0.5f;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private Transform target;

    private float currentHealth;
    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;

    private void Start()
    {
        EnemyAll.Instance.Characters.Add(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        // target = PlayerCash.Instance.Player.transform;
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (target != null)
        {
            navMeshAgent.SetDestination(target.position);

            Vector2 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            Debug.Log(Vector3.Distance(transform.position, target.position));
            Debug.Log(maxDistanceToPlayer);

            if (Vector3.Distance(transform.position, target.position) <= maxDistanceToPlayer)
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
    }
    
    public void InitializeSpecifications(int level)
    {        
        this.level = level;
        _exp = Random.Range(this.level, this.level + 5);
        maxHealth = Random.Range(10, this.level);
        currentHealth = maxHealth;
        damage = Random.Range(1, this.level + 5);
        moveSpeed = Random.Range(0.4f, this.level / 30); // с 80 на 30
        if(navMeshAgent == null)
            navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
    }

    public void SetAttackTarget(Transform target)
    {
        this.target = target;
    }

    private int maxHits = 2;
    private void Attack()
    {
        Collider2D[] hitColliders = new Collider2D[maxHits];
        int numColliders = Physics2D.OverlapCircleNonAlloc(transform.position, maxDistanceToPlayer, hitColliders, targetMask);

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
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            int randomSoundIndex = Random.Range(0, hurtSounds.Length-1);
            audioSource.PlayOneShot(hurtSounds[randomSoundIndex]);
        }
    }

    private void Die()
    {
    
        // Spawn loot if any available
        for (int i = 0; i < lootPrefabs.Length; i++)
        {
            float randomChance = Random.Range(0f, 1f);
            if (randomChance <= lootChances[i])
            {
                Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * _spawnRadius;
                Instantiate(lootPrefabs[i], spawnPosition, Quaternion.identity);
            }
        }

        GameObject destructionEffect = Instantiate(destructionParticlePrefab, transform.position, Quaternion.identity);

        // Add sound effect for death
        audioSource.PlayOneShot(deathSound);


        EnemyAll.Instance.Characters.Remove(this);
        Destroy(gameObject);
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, maxDistanceToPlayer);
    }
}