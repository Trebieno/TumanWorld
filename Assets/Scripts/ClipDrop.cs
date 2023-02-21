using UnityEngine;
using Feeling;

public class ClipDrop : MonoBehaviour
{
    private int _count;
    private void Start() 
    {
        _count = Random.Range(1, 2);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().AddClip(_count);
             AudioEffects.Instance.AudioTakeDrop.Play();
            Destroy(gameObject, AudioEffects.Instance.AudioTakeDrop.clip.length);            
        }
    }
}
