using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IAttackeble
{
    
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int curHealth;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float damage;

    [SerializeField] private int _level;
    [SerializeField] private int _exp;
    [SerializeField] private Player _player;
    
    [SerializeField] private List<GameObject> objects;

    private Rigidbody2D _rb;
    private Vector2 _movement;
    private AudioSource _audioDamage;
    [SerializeField] private AudioSource _audioDeath;
    [SerializeField] private SpriteRenderer _skin;
    [SerializeField] private ParticleSystem _particleDeath;
    

    private void Start()
    {
        _audioDamage = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();     
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _level = Random.Range((int)_player.GetLevel()/2, _player.GetLevel()+5);
        _exp = Random.Range(_player.GetLevel(), _player.GetLevel()+5);

        maxHealth = Random.Range(10, _level);
        curHealth = maxHealth;
        damage = Random.Range(1, _level+3);
        moveSpeed = Random.Range(0.4f, _level / 100);
    } 

    
    private void Update()
    {
        if(_player != null)
        {
            Vector3 direction = _player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _rb.rotation = angle;
            direction.Normalize();
            _movement = direction;
        }
    }

    private void FixedUpdate() 
    {
        if(_player != null)
        {
            MoveCharacter(_movement);
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        _rb.MovePosition(_rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.CompareTag("Player"))
        {
            _player.SetDamage((int)damage);
        }
    }

    public void SetDamage(int damage)
    {        
        curHealth -= damage;
        _audioDamage.Play();
        if (curHealth <= 0)
        {
            if(Random.Range(0, 100) <= 40)
            {
                int randomIndex = Random.Range(0, objects.Count);
                
                Instantiate(objects[randomIndex], transform.position, transform.rotation);                
            }
            
            _player.AddExp(_exp + _player.GetExpMultiplier());
            _player.CheckUpdateLevel();
            _player.UpdateSliders();
            
            _player = null;
            GetComponent<Collider2D>().enabled = false;
            _skin.enabled = false;
            _audioDeath.Play();
            Instantiate(_particleDeath, transform.position, transform.rotation);
            

            Destroy(gameObject, _audioDeath.clip.length);    
        }
    
        Debug.Log(curHealth +" "+ gameObject.name);
    }
}

