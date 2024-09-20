using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _direction;
    private float _speed = 13f;
    private float _rotateDegrees = 90f;
    private Action _action;

    private void Update() =>
        transform.position += _speed * Time.deltaTime * _direction;

    private void OnBecameInvisible() =>
        _action?.Invoke();

    public void SetDisableAction(Action action) =>
        _action = action;

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
        transform.rotation = direction.x < 0 ? new(0, 0, _rotateDegrees, 0) : new(0, 0, 0, 0);
    }
}