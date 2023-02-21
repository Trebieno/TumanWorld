using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Ore : MonoBehaviour
{
    [SerializeField] private int _maxOre;
    [SerializeField] private int _curOre;
    [SerializeField] private float _curResetTime;
    [SerializeField] private float _maxResetTime;
    [SerializeField] private TextMeshProUGUI _textCountOre;
    [SerializeField] private TextMeshProUGUI _textClickButton;    
    [SerializeField] private Slider _sliderMining;

    [SerializeField] private Player _player;
    private bool _isTrigger;
    private bool _isMining;
    private float _maxTimeMining => _player.Mining.GetTimeMiningOre();
    private float _curTimeMining;
    private bool _isZeroOre;

    [SerializeField] private AudioSource _audioMining;
    [SerializeField] private AudioSource _audioMiningFinaly;

    public event Action<int> OreChanged;

    public int CurrentOre
    {
        get
        {
            return _curOre;
        }

        set
        {
            CurrentOre = value;
            OreChanged?.Invoke(_curOre);
        }
    }

    private void Start() 
    {
        _audioMining = GetComponent<AudioSource>();
        _textClickButton = GameObject.Find("TQ").GetComponent<TextMeshProUGUI>();
        _curOre = UnityEngine.Random.Range(30, 100);
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        _curTimeMining = _player.Mining.GetTimeMiningOre();
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
                _curOre = UnityEngine.Random.Range(30, 100);
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
            _textCountOre.text = (_curOre -= 1 + _player.Mining.GetMiningMultiplier()).ToString();
            _player.Mining.AddOre(1 + _player.Mining.GetMiningMultiplier());
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
