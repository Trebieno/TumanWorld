using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using Feeling;
using Cinemachine;

public class Shooting : MonoCache
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefub;
    
    [SerializeField] private float _bulletForce;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private float _delayShoot;
    [SerializeField] private float _delayReload;

    [SerializeField] private int _clips;
    [SerializeField] private int _maximumAmmo;
    [SerializeField] private int _currentAmmo;

    [SerializeField] private Slider _reloadSlider;

    [SerializeField] private AudioSource _audioReload;
    [SerializeField] private AudioSource _audioFire;

    [Header("Camera Shake Settings")]
    [SerializeField] private CinemachineVirtualCamera _vCamera;
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    [SerializeField] private float _timeShake;

    [Header("Camera Shake Timer")]
    [SerializeField] private float _currentTimer;
    [SerializeField] private float _maximumTimer;

    private bool _isFire;
    private bool _isDelaingShoot;
    private bool _isDelaingReload;
    private Rigidbody2D _rb;
    private CinemachineBasicMultiChannelPerlin _cameraShake;

    public int MaximumAmmo => _maximumAmmo;
    public int CurrentAmmo => _currentAmmo;
    public int Clips {get {return _clips;} set{ _clips = value;}}
    public float BulletDamage => _bulletDamage;
    public float DelayShoot => _delayShoot;

    public event Action AmmoChanged;
    public event Action ClipChanged;
    
    private void Start()
    {
        StartCoroutine(Delay(_delayShoot));
        _reloadSlider.gameObject.SetActive(false);
        _audioReload.clip = AudioEffects.Instance.AudioReload;
        _audioFire.clip = AudioEffects.Instance.AudioFire;
    }

    public override void OnTick()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _isFire = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            _isFire = false;
        }

        if (_isFire)
        {
            if (!_isDelaingShoot)
            {
                StartCoroutine(Shake());
                Shoot();
                
            }
        }
        else if (!_isFire && _isDelaingReload)
        {
             _audioReload.Pause();
        }

        if (Input.GetKeyDown(KeyCode.R) && _clips > 0)
        {
            if (_currentAmmo < _maximumAmmo)
            {
                _currentAmmo = 0;
                AmmoChanged?.Invoke();
            }
        }

        if (_clips > 0 && _currentAmmo <= 0)
            {
                if (!_reloadSlider.gameObject.activeSelf)
                    _reloadSlider.gameObject.SetActive(true);

                if (!_audioReload.isPlaying)
                    _audioReload.Play();
                _reloadSlider.maxValue = _delayReload;
                _reloadSlider.value += Time.deltaTime;
                if (_reloadSlider.value >= _delayReload)
                {
                    Reload(_delayReload);
                }

                _isDelaingReload = true;
            }
    }

    private void Shoot()
    {
        if (_currentAmmo > 0)
        {
            _currentAmmo -= 1;
            AmmoChanged?.Invoke();
            _audioFire.Play();

            Bullet bullet = Instantiate(_bulletPrefub, _firePoint.position, _firePoint.rotation);
            // FeelingManager.Instance.ShootPosition = _firePoint.position;
            bullet.Damage = _bulletDamage;
            _rb = bullet.GetComponent<Rigidbody2D>();
            _rb.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);

            _isDelaingShoot = true;
            StartCoroutine(Delay(_delayShoot));
        }
    }

    private IEnumerator Shake ()
    {
        var camera = _vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        camera.m_AmplitudeGain = _amplitude;
        camera.m_FrequencyGain = _frequency;
        yield return new WaitForSeconds(_delayShoot);

        camera.m_AmplitudeGain = 0;
        camera.m_FrequencyGain = 0;
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _isDelaingShoot = false;
    }

    private void Reload(float delay)
    {
        _audioReload.Stop();
        _clips -= 1;
        _currentAmmo = _maximumAmmo;
        ClipChanged?.Invoke();
        AmmoChanged?.Invoke();
        _isDelaingReload = false;
        _reloadSlider.gameObject.SetActive(false);
        _reloadSlider.value = 0;
    }

    public void AddBulletDamage(float damage)
    {
        _bulletDamage += (_bulletDamage / 100) * damage;
    }

    public void AddSpeedFire(float delay)
    {
        _delayShoot -= (_delayShoot / 100) * delay;
    }

    public void AddClip(int clip)
    {
        _clips += clip;
        ClipChanged?.Invoke();
    }

    public void AddAmmo(int ammo)
    {
        _maximumAmmo += ammo;
        AmmoChanged?.Invoke();
    }

    public void SetAmmo(int ammo)
    {
        _maximumAmmo = ammo;
        if(_currentAmmo >= _maximumAmmo)
            _currentAmmo = _maximumAmmo;
    }

    public void SetDamage(float damage)
    {
        _bulletDamage = damage;
    }

    public void SetDelayShoot(float delay)
    {
        _delayShoot = delay;
    }
}