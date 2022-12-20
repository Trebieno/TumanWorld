using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ore : MonoBehaviour
{
    [SerializeField] private int _maxOre;
    [SerializeField] private int _curOre;
    [SerializeField] private float _curResetTime;
    [SerializeField] private float _maxResetTime;
    [SerializeField] private TextMeshProUGUI _textCountOre;
    [SerializeField] private TextMeshProUGUI _textClickButton;    
    [SerializeField] private Slider _sliderMining;

    private Player _player;
    private bool _isTrigger;
    private bool _isMining;
    private float _maxTimeMining => _player.GetTimeMiningOre();
    private float _curTimeMining;
    private bool _isZeroOre;

    [SerializeField] private AudioSource _audioMining;
    [SerializeField] private AudioSource _audioMiningFinaly;


    private void Start() 
    {
        _audioMining = GetComponent<AudioSource>();
        _textClickButton = GameObject.Find("TQ").GetComponent<TextMeshProUGUI>();
        _curOre = Random.Range(30, 100);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _curTimeMining = _player.GetTimeMiningOre();
        _textCountOre.text = _curOre.ToString();
        _sliderMining.gameObject.SetActive(false);
        _textCountOre.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Player"))
        {
            _isTrigger = true;
            _textClickButton.enabled = true;
            _textCountOre.enabled = true;
        }        
    }

    private void OnTriggerExit2D(Collider2D other) 
    {       
         if(other.transform.CompareTag("Player"))
         {
            _isTrigger = false;
            _textClickButton.enabled = false;
            _textCountOre.enabled = false;
            _isMining = false;
            _sliderMining.gameObject.SetActive(false);
         }
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Q) && _isTrigger)
        {
            _isMining = true;
            _textClickButton.enabled = false;
            if(_player.Pickaxe)
            {
                _textCountOre.text = _curOre.ToString();
                _sliderMining.gameObject.SetActive(true);
            }                
            else if(!_player.Pickaxe)
            {
                _textCountOre.text = "Нужна кирка";
            }
        }

        else if(Input.GetKeyUp(KeyCode.Q) && _isTrigger)
        {
            _isMining = false;
            _textClickButton.enabled = true;
            _sliderMining.gameObject.SetActive(false);
            _textCountOre.text = _curOre.ToString();
        }

        
        if(_isMining && _player.Pickaxe)
        {
            if(!_audioMining.isPlaying)
                _audioMining.Play();
            _sliderMining.maxValue = _curTimeMining;
            _sliderMining.value += Time.deltaTime;
            if(_sliderMining.value >= _sliderMining.maxValue )
                MiningOre();
        }

        if(_isZeroOre)
        {
            _curResetTime -= Time.deltaTime;
            if(_curResetTime <= 0)
            {
                _curOre = Random.Range(30, 100);
                _curResetTime = _maxResetTime;
                _textCountOre.text = _curOre.ToString();
                _isZeroOre = false;

            }

        }
    }

    private void MiningOre()
    {
        if(_curOre > 0)
        {
            _audioMiningFinaly.Play();
            _textCountOre.text = (_curOre -= 1 + _player.GetMiningMultiplier()).ToString();
            _player.AddOre(1 + _player.GetMiningMultiplier());
            _player.UpdateTextOre();
            _sliderMining.value = 0;
        }

        else
        {
            _isZeroOre = true;
        }
    }

    public void MineTurret()
    {
        if(!_audioMining.isPlaying)
            _audioMining.Play();
        _sliderMining.maxValue = _curTimeMining;
        _sliderMining.value += Time.deltaTime / 3;
        if(_sliderMining.value >= _sliderMining.maxValue )
            MiningOre();
    }
}
