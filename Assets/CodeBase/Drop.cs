using UnityEngine;
using Feeling;

public enum TypeDrop
{
    clip,
    light,
    turretAttack,
    turretMine,
    battory,
    ore,
    ship,
    money
}

public class Drop : MonoBehaviour
{
    [SerializeField] private AudioSource _audioTakeDrop;
    [SerializeField] private int _count;
    [SerializeField] private TypeDrop _drop;

    private void Start() 
    {
        _audioTakeDrop.clip = AudioEffects.Instance.AudioTakeDrop;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            PlayerCash.Instance.Player.AddDrop(_drop, _count);
             _audioTakeDrop.Play();
            Destroy(gameObject, _audioTakeDrop.clip.length);    
        }
    }
}
