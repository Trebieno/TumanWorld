using UnityEngine;
using TMPro;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private GameObject _flashLight;
    [SerializeField] private TextMeshProUGUI _timeFlashlight;
    private Player _player;

    [SerializeField] private float _maxPowerTime = 120;
    [SerializeField] private float _curPowerTime = 120;
    
    private void Start()
    {
        _player = GetComponent<Player>();
        _timeFlashlight.gameObject.SetActive(false);
    }

    
    private void Update()
    {
        if(_player.Flashlight)
        {
            if(Input.GetMouseButtonDown(1))
            {                
                _flashLight.SetActive(!_flashLight.activeSelf);
                _timeFlashlight.gameObject.SetActive(_flashLight.activeSelf);
            }

            if(_flashLight.activeSelf)
            {
                _curPowerTime -= Time.deltaTime;
                _timeFlashlight.text = $"{((int)_curPowerTime).ToString()}s";
                if(_curPowerTime <= 0)
                {
                    if(_player.BattoryCount > 0)
                    {
                        _player.BattoryCount -= 1;
                        _curPowerTime = _maxPowerTime;
                    }                        
                    else
                    {
                        _flashLight.SetActive(false);
                        _timeFlashlight.gameObject.SetActive(false);
                    }                        
                }                  
            }
        }
    }
}
