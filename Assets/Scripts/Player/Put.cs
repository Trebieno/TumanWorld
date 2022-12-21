using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Put : MonoBehaviour
{
    [SerializeField] private GameObject _lampPrefab;
    [SerializeField] private GameObject _shipPrefab;
    [SerializeField] private GameObject _mineTurretPrefab;
    [SerializeField] private GameObject _attackTurretPrefab;

    [SerializeField] private float _lampPutTime;
    [SerializeField] private float _shipPutTime;
    [SerializeField] private float _mineTurretPutTime;
    [SerializeField] private float _attackTurretPutTime;

    [SerializeField] private Slider _sliderBuild;
    [SerializeField] private AudioSource _audioBuild;
    [SerializeField] private AudioSource _audioFinaly;
    [SerializeField] private ParticleSystem _particleBuild;
    [SerializeField] private ScrollViewResourse _scrollViewResourse;

    private bool _isBuild;
    private Player _player;

    private void Start() 
    {
        _player = GetComponent<Player>();
        _sliderBuild.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(_player.ShipCount > 0 && _scrollViewResourse.Index == 0)
            Build(_shipPutTime, _shipPrefab, 0);

        else if(_player.LightCount > 0 && _scrollViewResourse.Index == 1)
            Build(_lampPutTime, _lampPrefab, 1);

        else if(_player.AttackTurretCount > 0 && _scrollViewResourse.Index == 2)
            Build(_attackTurretPutTime, _attackTurretPrefab, 2);
            
        else if(_player.MineTurretCount > 0 && _scrollViewResourse.Index == 3)
            Build(_mineTurretPutTime, _mineTurretPrefab, 3);        
    }


    private void Build(float time, GameObject prefab, int index)
    {
        if(Input.GetKeyDown(KeyCode.F) && !_isBuild)
        {
            // _audioBuild.Play();
            _sliderBuild.value = 0;    
            _sliderBuild.maxValue = time;                        
            _isBuild = true;
        }

        if(Input.GetKeyUp(KeyCode.F) && _isBuild)
        {
            // _audioBuild.Stop();
            _isBuild = false;
            _sliderBuild.value = 0;
            if(_sliderBuild.gameObject.activeSelf)
                _sliderBuild.gameObject.SetActive(false);
        }

        if(_sliderBuild.maxValue != time)
        {
            _sliderBuild.value = 0;
            _sliderBuild.maxValue = time;
        }

        if(_isBuild)
        {
            if(!_sliderBuild.gameObject.activeSelf)
                _sliderBuild.gameObject.SetActive(true);

            _sliderBuild.value += Time.deltaTime;
            if(_sliderBuild.value >= time)
            {
                // _audioBuild.Stop();
                _audioFinaly.Play();
                Instantiate(prefab, transform.position, transform.rotation);
                Instantiate(_particleBuild, transform.position, transform.rotation);
                _isBuild = false;
                _sliderBuild.gameObject.SetActive(false);
                _sliderBuild.value = 0;
                
                if(index == 0)
                    _player.ShipCount -= 1;
                
                if(index == 1)
                    _player.LightCount -= 1;
                
                if(index == 2)
                    _player.AttackTurretCount -= 1;
                
                if(index == 3)
                    _player.MineTurretCount -= 1;
                
                _player.UpdateScrollView();
            }                
        }
    }

    
}

