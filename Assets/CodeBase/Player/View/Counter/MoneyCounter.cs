using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using Feeling;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private Economic _economic;
    [SerializeField] private AudioMixer _mixer;
    private AudioSource _audioTaking;
    private AudioSource _audioLoss;
    private TMP_Text _textMoney;
    

    private void Awake()
    {
        _textMoney = GetComponent<TMP_Text>();
        _economic.MoneyChanged += Economic_OnMoneyChanged;
        _textMoney.text = _economic.Money.ToString("0");
    }

    private void Start()
    {
        _audioTaking = gameObject.AddComponent<AudioSource>();
        _audioLoss = gameObject.AddComponent<AudioSource>();
        
        AudioMixer audioMixer = Resources.Load<AudioMixer>("AudioMixer");
        AudioMixerGroup[] audioMixGroup = audioMixer.FindMatchingGroups("Effect");
        _audioTaking.outputAudioMixerGroup = audioMixGroup[0];
        _audioLoss.outputAudioMixerGroup = audioMixGroup[0];

        _audioTaking.clip = AudioEffects.Instance.AudioTakingFx;
        _audioLoss.clip =  AudioEffects.Instance.AudioLossFx;
    }

    private void OnDestroy()
    {
        _economic.MoneyChanged -= Economic_OnMoneyChanged;
    }

    private void Economic_OnMoneyChanged(float moneyValue)
    {
        if(_economic.Money > float.Parse(_textMoney.text))
            _audioTaking.Play();

        if(_economic.Money < float.Parse(_textMoney.text))
            _audioLoss.Play();

        _textMoney.text = _economic.Money.ToString("0.00");

    }
}