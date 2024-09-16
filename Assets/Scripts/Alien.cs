using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _fireRate = 5f;
    [SerializeField] private float _moveSpeed = 0.07f;

    private Vector2 _targetPoint;
    private Coroutine _coroutine;
    private Action _disable;
    private float _halfSize;
    private float _divider = 2;

    private void Start() =>
        _halfSize = GetComponent<SpriteRenderer>().bounds.size.x / _divider;

    private void Update()
    {
        if (transform.position == _targetPoint.ConvertTo<Vector3>() && _coroutine == null)
            _coroutine = StartCoroutine(Fire());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            _coroutine = null;
    }

    private void FixedUpdate() =>
        transform.position = Vector2.MoveTowards(transform.position, _targetPoint, _moveSpeed);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponentInChildren<Bullet>();

        if (bullet == null)
            return;

        _disable?.Invoke();
    }

    public void SetDisableAction(Action disable) =>
        _disable = disable;

    public void SetTargetPoint(Vector2 targetPoint) =>
        _targetPoint = targetPoint;

    private IEnumerator Fire()
    {
        WaitForSeconds wait = new(_fireRate);

        while (true)
        {
            Vector3 firePoint = new(-_halfSize - 1, 0, 0);
            firePoint += transform.position;

            Bullet bullet = Instantiate(_bullet, firePoint, Quaternion.identity);
            bullet.SetDirection(Vector3.left);

            yield return wait;
        }
    }
}