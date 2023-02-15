using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D Rb => _rb;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Camera _camera;

    [SerializeField] private AudioSource _audioSteps;

    private Vector2 _movement;
    private Vector2 _mousePosition;
    private PlayerContol _input;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _input = new PlayerContol();
    }

    private void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = _mousePosition - _rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        _rb.rotation = angle;
    }

    public void AddSpeedMovement(float speed)
    {
        _moveSpeed += speed;
    }
}