using UnityEngine;

public class Ship : ObjectGame
{
    [SerializeField] private int _damage;
    [SerializeField] private int _charge;
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
            if(_charge > 0)
            {
                _audio.Play();
                EnemyAll.Instance.Characters.Find(x => x.transform == other.transform).SetDamage(_damage, null);
                _charge -= 1;
                if(_charge == 0)
                    Destroy(gameObject, _audio.clip.length);
            }
        }
    }
}