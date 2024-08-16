using UnityEngine;

public class Bullet : MonoBehaviour, IDamageSource
{
    private Vector3 _direction;
    private float _speed = 0.2f;
    private float _rotateDegrees = 90f;

    [field: SerializeField] public float Damage { get; private set; }

    private void Update()
    {

    }

    private void FixedUpdate() =>
        transform.position += _direction * _speed;

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
        transform.rotation = direction.x < 0 ? new(0, 0, _rotateDegrees, 0) : new(0, 0, 0, 0);
    }
}