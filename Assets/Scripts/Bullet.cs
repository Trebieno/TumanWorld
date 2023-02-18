using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    [SerializeField] private float _bulletDestroy;

    private void Start() 
    {
        Destroy(gameObject, _bulletDestroy);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.CompareTag("Enemy"))
        {
            other.transform.GetComponent<Character>().SetDamage(damage);
        }
        Destroy(gameObject);
    }
}
