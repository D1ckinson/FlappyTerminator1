using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Jump))]
public class Spaceship : MonoBehaviour
{
    [SerializeField] private FireButton _fireButton;
    [SerializeField] private DamageHandler _damageHandler;
    [SerializeField] private Weapon _weapon;

    private Rigidbody2D _rigidbody;
    private float _velocityMultiplier = 3.5f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _damageHandler.SetAction(() => Destroy(gameObject));
        _weapon.SetDirection(true);
    }

    private void FixedUpdate()
    {
        float degree = _rigidbody.velocity.y * _velocityMultiplier;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private void OnEnable() =>
        _fireButton.Fire += _weapon.Fire;

    private void OnDisable() =>
        _fireButton.Fire -= _weapon.Fire;
}
