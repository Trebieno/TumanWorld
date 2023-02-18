using UnityEngine;

public class Money : MonoBehaviour
{
    private int _countMoney;
    [SerializeField] private AudioSource _audioTake;

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
            _audioTake.Play();
            Destroy(gameObject, _audioTake.clip.length);
        }
    }
}