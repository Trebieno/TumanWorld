using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    [SerializeField] private float _bulletDestroy;
    public Turret Turret;

    private void Start() 
    {
        Destroy(gameObject, _bulletDestroy);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.CompareTag("Enemy"))
        {
            SpawnPoints.Instance.SpawnPointsList.RemoveAll(x => x == null);
            EnemyAll.Instance.Characters.RemoveAll(x => x == null);
            var enemy = EnemyAll.Instance.Characters.Find(x => x.transform == other.transform);
            var spawn = SpawnPoints.Instance.SpawnPointsList.Find(x => x.transform == other.transform);

            if(enemy == null) spawn.SetDamage(Damage, Turret);
            else enemy.SetDamage(Damage, Turret);
        }
        Destroy(gameObject);
    }
}
