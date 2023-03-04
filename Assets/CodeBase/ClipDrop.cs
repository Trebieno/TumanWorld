using UnityEngine;
using Feeling;

public class ClipDrop : MonoBehaviour
{
    [SerializeField] private AudioSource _audioTakeDrop;
    private int _count;

    private void Start() 
    {
        _count = Random.Range(1, 2);
        _audioTakeDrop.clip = AudioEffects.Instance.AudioTakeDrop;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().AddClip(_count);
             _audioTakeDrop.Play();
            Destroy(gameObject, _audioTakeDrop.clip.length);            
        }
    }
}
