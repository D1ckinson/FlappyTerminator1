using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 10f;

    public bool IsJump = false;
    private Rigidbody2D _rigidbody;

    private void Start() => 
        _rigidbody = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        if (IsJump)
        {
            Jump();
            IsJump = false;
        }
    }

    private void Jump() => 
        _rigidbody.velocity = new(_rigidbody.velocity.x, _jumpForce);
}
