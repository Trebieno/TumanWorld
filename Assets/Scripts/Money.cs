using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    private int _countMoney;
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private AudioSource _audioTake;

    private void Start() 
    {
        _audioTake = GetComponent<AudioSource>();
        _text = GameObject.Find("TMoney").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Money += Random.Range(0, 10 + other.GetComponent<Player>().MoneyMultiplier);
            _text.text = other.GetComponent<Player>().Money.ToString();
            _audioTake.Play();
            Destroy(gameObject, _audioTake.clip.length);
            
        }
    }
}
