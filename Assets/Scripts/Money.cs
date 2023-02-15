using UnityEngine;

public class Money : MonoBehaviour
{
    private int _countMoney;
    [SerializeField] private AudioSource _audioTake;

    private void Start()
    {
        _audioTake = GetComponent<AudioSource>();
        _countMoney = Random.Range(1, 10);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Economic>().Money += _countMoney + other.GetComponent<Economic>().MoneyMultiplier;
            _audioTake.Play();
            Destroy(gameObject, _audioTake.clip.length);
        }
    }
}