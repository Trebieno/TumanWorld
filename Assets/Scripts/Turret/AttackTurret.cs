using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AttackTurret : Turret
{
    [Range(0, 360)] private float ViewAngle = 90f;
    private float ViewDistance = 15f;
    private float DetectionDistance = 3f;
    public Transform Target;

    [SerializeField] private float rotationSpeed;
    private Transform agentTransform;
    private Rigidbody2D _rb;
    [SerializeField] private Transform _dulo;
    [SerializeField] private List<Transform> targets = new List<Transform>();
    
    [SerializeField] private AudioSource _audioShoot;
    [SerializeField] private AudioSource _audioReload;
    
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Bullet _bulletPrefub;
    [SerializeField] private float _bulletForce;
    [SerializeField] private float _bulletDamage;

    [SerializeField] private float _curDelayShoot;
    [SerializeField] private float _maxDelayShoot;

    [SerializeField] private float _delayReload;
    [SerializeField] private int _maxAmmo = 30;
    [SerializeField] private int _curAmmo = 30;

    [SerializeField] private Slider _reloadSlider;

    [SerializeField] private bool _isFire = false;
    private bool _isDelaingShoot;
    private bool _isDelaingReload;

    //мертвая зона вращения (чтобы турель не дергалась при x=0)
    private float _deadZone = 0.1F;
    //направление вращения ( "0" - не вращать, "1" - вправо и "-1" - влево)
    private float _rotateDirection = 0;

    private void Start()
    {
        rotationSpeed = 2;
        agentTransform = transform;
        isPower = false;
        indicatorActive.SetActive(isPower);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();        
        s_Turrets.Add(this);
        UpdateUIHealth();
    }

    private void Update() 
    {
        Checking();           
        
        DrawViewState();

        if(Target != null && isPower)
        {
            if(_dulo.InverseTransformPoint(Target.position).x > _deadZone/2) _rotateDirection = -1F;

            else if (_dulo.InverseTransformPoint(Target.position).x < -_deadZone/2) _rotateDirection = 1F;

            else
            {
                if (_dulo.InverseTransformPoint(Target.position).y<0) _rotateDirection = 1F;
                else
                {
                    _rotateDirection = 0;
                    
                    if(!_isDelaingShoot)
                    {
                        _isFire = true;
                    }           
                    else
                    {
                        _isFire = false;                        
                    }         
                } 
            }
                    
            _dulo.rotation *= Quaternion.Euler(0,0,rotationSpeed * _rotateDirection);
        }
    }

    

    public override bool Action(bool player)
    {
        player = false;
        foreach (var item in hitCollidersTargets)
        {
            Character character = item.GetComponent<Character>();
            if(character != null)
            {
                targets.RemoveAll(x => x == null);
                targets.RemoveAll(x => x == character.transform);
                targets.Add(character.transform);
                

                if(Target != null)
                {
                    float distanceToTarget = Vector3.Distance(Target.transform.position, transform.position);
                    // if (distanceToTarget <= DetectionDistance || IsInView())  
                    // {
                    //     Debug.Log("111");
                    //     // RotateToTarget();
                    // }

                    if(_audioReload.isPlaying)
                        _audioReload.Pause();                   
                }
            }            
        }

        player = false;
        foreach (var item in hitCollidersPlayer)
        {
            if(item.CompareTag("Player"))
            {
                if(Input.GetKeyDown(KeyCode.Z))
                {                    
                    isPower = !isPower;
                    indicatorActive.SetActive(isPower);
                }
                    
                player = true;
                textPowerTime.gameObject.SetActive(true);
                textPowerTime.text = ((int)curPowerTime).ToString();
            }
        }

        if(_isFire && !_isDelaingShoot)
        {
            Shoot();
        }

        if(_isDelaingShoot)
        {            
            _isDelaingShoot = true;
            // Delay(_curDelayShoot);
            base.player.UpdateUI();
            _curDelayShoot -= Time.deltaTime;
            if(_curDelayShoot <= 0)
                _isDelaingShoot = false;
        }

        if(Target == null)
        {
            _isFire = false;
            if(_audioReload.isPlaying)
                _audioReload.Pause(); 
        }

        targets.RemoveAll(x => x == null);      
        if(targets.Count > 0)
        {
            targets = targets.Where(x => Vector3.Distance(transform.position, x.transform.position) < radiusTargets).ToList();
            targets = targets.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).ToList();

            targets.RemoveAll(x => x == null);
            try
            {
                Target = targets[0];
            }
            catch
            {
                
            }
            
        }
        else
        {
            if(Target != null)
            {
                Target = null;
            }
        }
        
        return player;
    }

    private void Shoot()
    {
        if (_curAmmo > 0 && !_isDelaingShoot)
        {
            _curAmmo -= 1;
            _audioShoot.Play();
            Bullet bullet = Instantiate(_bulletPrefub, _firePoint.position, _firePoint.rotation);        
            bullet.damage = _bulletDamage;
            _rb = bullet.GetComponent<Rigidbody2D>();
            _rb.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);

            _isDelaingShoot = true;
            _curDelayShoot = _maxDelayShoot;
        }

        else if(_curAmmo <= 0)
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


    private void Reload(float delay)
    {
        _audioReload.Stop();
        _curAmmo = _maxAmmo;
        player.UpdateUI();
        _isDelaingReload = false;
        _reloadSlider.gameObject.SetActive(false);
        _reloadSlider.value = 0;
    }

    private bool IsInView() // true если цель видна
    {
        float realAngle = Vector3.Angle(_dulo.up, Target.position - _dulo.position);
        RaycastHit2D hit = Physics2D.Raycast(_dulo.position, Target.position - _dulo.position, ViewDistance);
        if (hit)
        {
            if (realAngle < ViewAngle / 2f && Vector3.Distance(_dulo.position, Target.position) <= ViewDistance && hit.transform == Target.transform)
            {
                return true;
            }
        }
        return false;
    }
    private void RotateToTarget() // поворачивает в стороно цели со скоростью rotationSpeed
    {
        Vector3 lookVector = Target.position - agentTransform.position;
        float angle = Mathf.Atan2(lookVector.y,lookVector.x) * Mathf.Rad2Deg - 90;
        //LateUpdate();  
    }

    private void LateUpdate() 
    {
        
    }
    
    
    private void DrawViewState() 
    {       
        Vector3 left = _dulo.position + Quaternion.Euler(new Vector3(0,0,ViewAngle / 2f)) * (_dulo.up * ViewDistance);
        Vector3 right = _dulo.position + Quaternion.Euler(-new Vector3(0,0,ViewAngle / 2f)) * (_dulo.up * ViewDistance);     
        Debug.DrawLine(_dulo.position, left, Color.yellow);
        Debug.DrawLine(_dulo.position, right, Color.yellow);       
    }


}
