using UnityEngine;

public class Ship : ObjectGame
{
    [SerializeField] private int _damage;
    private AudioSource _audio;

    private void Awake()
    {
        typeObject = GameObjects.Ship;
    }
    private void Start() 
    {
        Objects.Instance.ObjectsGame.Add(this);
        _audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            if(curHealth > 0)
            {
                _audio.Play();
                EnemyAll.Instance.Characters.RemoveAll(x => x == null);
                EnemyAll.Instance.Characters.Find(x => x.transform == other.transform).SetDamage(_damage, null);
                curHealth -= 1;
                if(curHealth == 0)
                    Destroy(gameObject, _audio.clip.length);
            }
        }
    }
}