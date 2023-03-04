using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class AudioVolume : MonoBehaviour
{
    public static AudioVolume Instance { get; private set; }

    private const float _multiplier = 20;
    [SerializeField] private AudioMixer _mixer;

    [Header(" ")]
    private string _volumeParameterAll = "All";
    [SerializeField] private Slider _sliderVolumeAll;
    [SerializeField] private TMP_Text _textVolumeAll;
    private float _volumeValueAll;

    [Header(" ")]
    private string _volumeParameterMusic = "Music";
    [SerializeField] private Slider _sliderVolumeMusic;
    [SerializeField] private TMP_Text _textVolumeMusic;
    private float _volumeValueMusic;

    [Header(" ")]
    private string _volumeParameterEffect = "Effect";
    [SerializeField] private Slider _sliderVolumeEffect;
    [SerializeField] private TMP_Text _textVolumeEffect;
    private float _volumeValueEffect;



    private void Awake()
    {
        _sliderVolumeAll.onValueChanged.AddListener(All_OnVolumeChanged);

        _sliderVolumeMusic.onValueChanged.AddListener(Music_OnVolumeChanged);

        _sliderVolumeEffect.onValueChanged.AddListener(Effect_OnVolumeChanged);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {   
        _volumeValueAll = PlayerPrefs.GetFloat(_volumeParameterAll, Mathf.Log10(_sliderVolumeAll.value) * _multiplier);
        if(_volumeValueAll == 0) _volumeValueAll = 0.001f;
        _sliderVolumeAll.value = Mathf.Pow(10, _volumeValueAll / _multiplier);

        _volumeValueMusic = PlayerPrefs.GetFloat(_volumeParameterMusic, Mathf.Log10(_sliderVolumeMusic.value) * _multiplier);
        if(_volumeValueMusic == 0) _volumeValueMusic = 0.001f;
        _sliderVolumeMusic.value = Mathf.Pow(10, _volumeValueMusic / _multiplier);

        _volumeValueEffect = PlayerPrefs.GetFloat(_volumeParameterEffect, Mathf.Log10(_sliderVolumeEffect.value) * _multiplier);
        if(_volumeValueEffect == 0) _volumeValueEffect = 0.001f;
        _sliderVolumeEffect.value = Mathf.Pow(10, _volumeValueEffect / _multiplier);

        
        
        

        All_OnVolumeChanged(_volumeValueAll);
        Music_OnVolumeChanged(_volumeValueMusic);
        Effect_OnVolumeChanged(_volumeValueEffect);
    }

    private void All_OnVolumeChanged(float value)
    {        
        _volumeValueAll = Mathf.Log10(value) * _multiplier;
        _mixer.SetFloat(_volumeParameterAll, _volumeValueAll);
        _textVolumeAll.text = (_sliderVolumeAll.value * 100).ToString("0.0");
    }

    private void Music_OnVolumeChanged(float value)
    {
        _volumeValueMusic = Mathf.Log10(value) * _multiplier;
        _mixer.SetFloat(_volumeParameterMusic, _volumeValueMusic);
        _textVolumeMusic.text = (_sliderVolumeMusic.value * 100).ToString("0.0");
    }

    private void Effect_OnVolumeChanged(float value)
    {
        _volumeValueEffect = Mathf.Log10(value) * _multiplier;
        _mixer.SetFloat(_volumeParameterEffect, _volumeValueEffect);
        _textVolumeEffect.text = (_sliderVolumeEffect.value * 100).ToString("0.0");
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameterAll, _volumeValueAll);
        PlayerPrefs.SetFloat(_volumeParameterMusic, _volumeValueMusic);
        PlayerPrefs.SetFloat(_volumeParameterEffect, _volumeValueEffect);
    }
}
