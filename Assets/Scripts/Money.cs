using UnityEngine;
using Feeling;

public class Money : MonoBehaviour
{
    [SerializeField] private AudioSource _audioTakeDrop;
    private int _countMoney;

    private void Start()
    {
        _countMoney = Random.Range(1, 10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var economic = other.GetComponent<Economic>();
            
            economic.Money += _countMoney + economic.MoneyMultiplier;
            _audioTakeDrop.Play();
            Destroy(gameObject, _audioTakeDrop.clip.length);
        }
    }
}