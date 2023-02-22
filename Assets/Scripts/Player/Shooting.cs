using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Feeling;

public class Shooting : MonoBehaviour
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

    private bool _isFire;
    private bool _isDelaingShoot;
    private bool _isDelaingReload;
    private Rigidbody2D _rb;

    public int MaximumAmmo => _maximumAmmo;
    public int CurrentAmmo => _currentAmmo;
    public int Clips => _clips;

    public event Action AmmoChanged;
    public event Action ClipChanged;
    
    private void Start()
    {
        StartCoroutine(Delay(_delayShoot));
        _reloadSlider.gameObject.SetActive(false);
    }

    private void Update()
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
                Shoot();
            }
        }
        else if (!_isFire && _isDelaingReload)
        {
             AudioEffects.Instance.AudioReload.Pause();
        }

        if (Input.GetKeyDown(KeyCode.R) && _clips > 0)
        {
            if (_currentAmmo < _maximumAmmo)
            {
                _currentAmmo = 0;
                AmmoChanged?.Invoke();
            }
        }
    }

    private void Shoot()
    {
        if (_currentAmmo > 0)
        {
            _currentAmmo -= 1;
            AmmoChanged?.Invoke();
            AudioEffects.Instance.AudioFire.Play();

            Bullet bullet = Instantiate(_bulletPrefub, _firePoint.position, _firePoint.rotation);
            bullet.damage = _bulletDamage;
            _rb = bullet.GetComponent<Rigidbody2D>();
            _rb.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);

            _isDelaingShoot = true;
            StartCoroutine(Delay(_delayShoot));
        }
        else
        {
            if (_clips > 0)
            {
                if (!_reloadSlider.gameObject.activeSelf)
                    _reloadSlider.gameObject.SetActive(true);

                if (!AudioEffects.Instance.AudioReload.isPlaying)
                    AudioEffects.Instance.AudioReload.Play();
                _reloadSlider.maxValue = _delayReload;
                _reloadSlider.value += Time.deltaTime;
                if (_reloadSlider.value >= _delayReload)
                {
                    Reload(_delayReload);
                }

                _isDelaingReload = true;
            }
        }
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _isDelaingShoot = false;
    }

    private void Reload(float delay)
    {
        AudioEffects.Instance.AudioReload.Stop();
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
        _bulletDamage += damage;
    }

    public void AddSpeedFire(float delay)
    {
        _delayShoot -= delay;
    }

    public void AddClip(int clip)
    {
        _clips += clip;
        ClipChanged?.Invoke();
    }

    public void AddAmmo(int ammo)
    {
        _maximumAmmo += ammo;
    }
}