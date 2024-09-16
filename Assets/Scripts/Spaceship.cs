using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Mover))]
public class Spaceship : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private FireButton _fireButton;

    private float _yMinusDeadZone;
    private Rigidbody2D _rigidbody;
    private float _velocityMultiplier = 3.5f;
    private float _halfSize;
    private float _divider = 2;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        Vector3 cameraToObject = transform.position - Camera.main.transform.position;
        float distance = Vector3.Project(cameraToObject, Camera.main.transform.forward).z;

        Vector3 leftBot = Camera.main.ViewportToWorldPoint(new(0, 0, distance));

        _yMinusDeadZone = leftBot.y;

        _halfSize = GetComponent<SpriteRenderer>().bounds.size.x / _divider;
    }

    private void Update()
    {
        if (transform.position.y < _yMinusDeadZone)
            Die();
    }

    private void FixedUpdate()
    {
        float degree = _rigidbody.velocity.y * _velocityMultiplier;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponentInChildren<Bullet>();

        if (bullet == null)
            return;

        Die();
    }

    private void OnEnable() =>
        _fireButton.Fire += Fire;

    private void OnDisable() =>
        _fireButton.Fire -= Fire;

    private void Fire()
    {
        Vector3 firePoint = new(_halfSize + 1, 0, 0);
        firePoint += transform.position;

        Bullet bullet = Instantiate(_bullet, firePoint, Quaternion.identity);
        bullet.SetDirection(Vector3.right);
    }

    private void Die() =>
        Destroy(gameObject);
}
