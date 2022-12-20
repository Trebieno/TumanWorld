using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _charge;
    private AudioSource _audio;

    private void Start() 
    {
        _audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Enemy"))
        {
            if(_charge > 0)
            {
                _audio.Play();
                other.GetComponent<Character>().SetDamage(_damage);
                _charge -= 1;
                if(_charge == 0)
                    Destroy(gameObject, _audio.clip.length);
            }
        }
    }
}
