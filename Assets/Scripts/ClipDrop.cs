using UnityEngine;

public class ClipDrop : MonoBehaviour
{
    private int _count;
    [SerializeField] private AudioSource _audioTake;
    private void Start() 
    {
        _audioTake = GetComponent<AudioSource>();
        _count = Random.Range(1, 2);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().AddClip(_count);
            other.GetComponent<Player>().UpdateAmmo();
            _audioTake.Play();
            Destroy(gameObject, _audioTake.clip.length);            
        }
    }
}
