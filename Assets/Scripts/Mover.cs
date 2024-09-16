using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;

    public bool IsJump = false;
    public event UnityAction Jumping;
    private Rigidbody2D _rigidbody;

    private void Start() => 
        _rigidbody = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        if (IsJump)
        {
            Jump();
            Jumping?.Invoke();
            IsJump = false;
        }
    }

    private void Jump() => 
        _rigidbody.velocity = new(_rigidbody.velocity.x, _jumpForce);
}
