using UnityEngine;
using Feeling;

public class Movement : MonoBehaviour
{

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maximumTimeStep;

    [SerializeField] private float _currentTimeStep;
    private Vector2 _movement;
    private Vector2 _mousePosition;
    private PlayerContol _input;
    private int _index = 0;

    public Rigidbody2D Rb => _rb;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _input = new PlayerContol();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        _movement = new Vector3(x, y, 0) * _moveSpeed;
        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        if(x != 0 || y != 0 )
        {
            if(_currentTimeStep <= 0)
            {
                AudioEffects.Instance.AudioSteps[_index].gameObject.SetActive(false);

                _index = Random.Range(0, AudioEffects.Instance.AudioSteps.Length - 1);            
                
                AudioEffects.Instance.AudioSteps[_index].gameObject.SetActive(true);

                _currentTimeStep = _maximumTimeStep;
            }
        }

        else if(x == 0 && y == 0)
        {
            AudioEffects.Instance.AudioSteps[_index].gameObject.SetActive(false);
        }

        if(_currentTimeStep > 0)
            _currentTimeStep -= Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
        // _rb.velocity = _movement;

        Vector2 lookDir = _mousePosition - _rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        _rb.rotation = angle;
    }

    public void AddSpeedMovement(float speed)
    {
        _moveSpeed += speed;
    }
}