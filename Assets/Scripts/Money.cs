using System.Collections;
using System.Collections.Generic;
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
        _countMoney = Random.Range(1, 10);
        _text = GameObject.Find("TMoney").GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Money += _countMoney + other.GetComponent<Player>().GetMoneyMultiplier();
            _text.text = other.GetComponent<Player>().Money.ToString();
            _audioTake.Play();
            Destroy(gameObject, _audioTake.clip.length);
            
        }
    }
}
