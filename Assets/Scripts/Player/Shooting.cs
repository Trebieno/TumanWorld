using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefub;
    [SerializeField] private float _bulletForce;
    [SerializeField] private float _bulletDamage;
    [SerializeField] private float _delayShoot;
    [SerializeField] private float _delayReload;
    [SerializeField] private int _clips;
    [SerializeField] private int _maxAmmo;
    [SerializeField] private int _curAmmo;
    [SerializeField] private TextMeshProUGUI _textAmmo;
    [SerializeField] private TextMeshProUGUI _textClip;
    [SerializeField] private TextMeshProUGUI _textMoney;
    [SerializeField] private Slider _reloadSlider;

    [SerializeField] private AudioSource _audioShoot;
    [SerializeField] private AudioSource _audioReload;

    private bool _isFire;
    private bool _isDelaingShoot;
    private bool _isDelaingReload;
    private Rigidbody2D _rb;

    private void Start() 
    {
        _audioShoot = GetComponent<AudioSource>();
        StartCoroutine(Delay(_delayShoot));
        _reloadSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            _isFire = true;            
        }
        if(Input.GetButtonUp("Fire1"))
        {
            _isFire = false;            
        }

        if(_isFire)
        {
            if(!_isDelaingShoot)
            {
                Shoot();
            }            
        }
        else if (!_isFire && _isDelaingReload)
        {
            _audioReload.Pause();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if(_curAmmo < _maxAmmo)
            {
                _curAmmo = 0;
                UpdateAmmo();
            }
        }
    }

    private void Shoot()
    {
        
        if (_curAmmo > 0)
        {
            _curAmmo -= 1;
            _audioShoot.Play();
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
                if(!_reloadSlider.gameObject.activeSelf)
                    _reloadSlider.gameObject.SetActive(true);
                
                if(!_audioReload.isPlaying)
                    _audioReload.Play();
                _reloadSlider.maxValue = _delayReload;
                _reloadSlider.value += Time.deltaTime;
                if(_reloadSlider.value >= _delayReload)
                {
                    Reload(_delayReload);
                } 
                _isDelaingReload = true;
                
            }
        }
    }

    private IEnumerator Delay(float delay)
    {
        UpdateAmmo();
        UpdateClip();
        yield return new WaitForSeconds(delay);
        _isDelaingShoot = false;
    }

    private void Reload(float delay)
    {
        _audioReload.Stop();
        _clips -= 1;
        _curAmmo = _maxAmmo;
        UpdateAmmo();
        UpdateClip();
        _isDelaingReload = false;
        _reloadSlider.gameObject.SetActive(false);
        _reloadSlider.value = 0;
        

    }

    public void AddBulletDamage (float damage) 
    {
        _bulletDamage += damage;
    }

   public void AddSpeedFire (float delay) 
   {
        _delayShoot -= delay;
   }
   
   public void AddClip(int clip)
   {
        _clips += clip;
        UpdateClip();
   }

   public void AddAmmo(int ammo)
   {
        _maxAmmo += ammo;
   }

   public void UpdateAmmo()
   {
        _textAmmo.text = $"{_curAmmo} / {_maxAmmo}";
   }

   public void UpdateClip()
   {
        _textClip.text = _clips.ToString();
   }
}
