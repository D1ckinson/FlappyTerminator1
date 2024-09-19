using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
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
            Hop();
            Jumping?.Invoke();
            IsJump = false;
        }
    }

    private void Hop() => 
        _rigidbody.velocity = new(_rigidbody.velocity.x, _jumpForce);
}
