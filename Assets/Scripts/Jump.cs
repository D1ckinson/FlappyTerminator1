using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Jump : MonoBehaviour
{
    [SerializeField] private float _force = 10f;

    public bool IsHop = false;
    private Rigidbody2D _rigidbody;

    public event UnityAction Hoping;

    private void Awake() => 
        _rigidbody = GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        if (IsHop)
        {
            Hop();
            Hoping?.Invoke();
            IsHop = false;
        }
    }

    private void Hop() => 
        _rigidbody.velocity = new(_rigidbody.velocity.x, _force);
}
