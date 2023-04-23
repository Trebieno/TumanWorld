using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Feeling;

public class Character : MonoBehaviour, IAttackeble
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float curHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float damage;
    [SerializeField] protected float maxDistanceToPlayer;

    [Header("Culldown attack")] [SerializeField]
    private float _maxCulldown;

    [SerializeField] private float _curCulldown;

    [Space(10)] [SerializeField] private int _level;
    [SerializeField] private int _exp;
    [SerializeField] private Transform _target;

    [Space(10)] [SerializeField] private List<GameObject> lootObjects;

    private Rigidbody2D _rb;
    private Vector2 _movement;
    private AudioSource _audioDamage;
    private Player _player;
    private NavMeshAgent _agent;    

    [Space(10)] [SerializeField] private AudioSource _audioDeath;
    [SerializeField] private SpriteRenderer _skin;
    [SerializeField] private ParticleSystem _particleDeath;

    [Space(10)] [SerializeField] private LayerMask _attackMask;
    [SerializeField] private float _radius;

    [Space(10)] [SerializeField] private Transform _attackPoint;

    private Collider2D _colliderTarget;
    private Collider2D _collider;


    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _player = PlayerCash.Instance.Player;
        _audioDamage = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        
        EnemyAll.Instance.Characters.Add(this);
    }

    public void InitializeSpecifications(int level)
    {        
        _level = level;
        _exp = Random.Range(_level, _level + 5);
        maxHealth = Random.Range(10, _level);
        curHealth = maxHealth;
        damage = Random.Range(1, _level + 3);
        moveSpeed = Random.Range(0.7f, _level / 80); // с 100 на 80 
        if(_agent == null)
            _agent = GetComponent<NavMeshAgent>();
        _agent.speed = moveSpeed;
    }

    private void Update()
    {
        if (_target != null)
        {
            _agent.SetDestination(_target.position);
            Vector3 direction = _target.transform.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rb.rotation = angle;
            direction.Normalize();
            _movement = direction;
        }
        else if (_target == null && TurretsAll.Instance.Turrets.Count > 0)
        {
            TurretsAll.Instance.Turrets.RemoveAll(x => x == null);
            int rnd = Random.Range(0, 100);
            if (30 >= rnd)
                _target = TurretsAll.Instance.Turrets[Random.Range(0, TurretsAll.Instance.Turrets.Count - 1)].transform;
            else
                _target = _player.transform;
        }
        else
        {
            _target = _player.transform;
        }
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            // MoveCharacter(_movement);
        }

        if (_curCulldown > 0)
            _curCulldown -= Time.fixedDeltaTime;

        _collider = Physics2D.OverlapCircle(_attackPoint.position, _radius, _attackMask);

        if (_collider != null && _curCulldown <= 0)
        {
            _collider.GetComponent<IAttackeble>().SetDamage(damage);
            _curCulldown = _maxCulldown;
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        _rb.MovePosition(_rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _radius);
    }

    public void SetDamage(float damage)
    {
        curHealth -= damage;
        _audioDamage.Play();
        if (curHealth <= 0)
        {
            if (Random.Range(0, 100) <= 40)
            {
                int randomIndex = Random.Range(0, lootObjects.Count);

                Instantiate(lootObjects[randomIndex], transform.position, transform.rotation);
            }
            

            _player.Leveling.AddExp(_exp + _player.Leveling.ExpirienceMultiplier);
            _player.Leveling.CheckUpdateLevel();

            _player = null;
            _collider.enabled = false;
            _skin.enabled = false;
            _audioDeath.Play();
            Instantiate(_particleDeath, transform.position, transform.rotation);


            Destroy(gameObject, _audioDeath.clip.length);
        }
    }
}