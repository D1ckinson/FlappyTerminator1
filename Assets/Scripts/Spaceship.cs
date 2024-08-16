using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Spaceship : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health = 100;

    private float _yMinusDeadZone;
    private Rigidbody2D _rigidbody;
    private float _velocityMultiplier = 3.5f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        Vector3 cameraToObject = transform.position - Camera.main.transform.position;
        float distance = Vector3.Project(cameraToObject, Camera.main.transform.forward).z;

        Vector3 leftBot = Camera.main.ViewportToWorldPoint(new(0, 0, distance));

        _yMinusDeadZone = leftBot.y;
    }

    private void Update()
    {
        if (transform.position.y < _yMinusDeadZone)
        {
            Debug.Log("работает");
        }
    }

    private void FixedUpdate()
    {
        float degree = _rigidbody.velocity.y * _velocityMultiplier;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageSource source = collision.GetComponentInChildren<IDamageSource>();
        Debug.Log("Бум");
        if (source == null)
            return;

        TakeDamage(source.Damage);
        Destroy(collision.gameObject);
        Debug.Log(_health);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }
}
