using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Alien : MonoBehaviour, IDamageable
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _fireRate = 5f;
    [SerializeField] private float _health = 100;
    [SerializeField] private float _moveSpeed = 0.07f;

    private Vector2 _targetPoint;
    private Coroutine _coroutine;

    //private void OnEnable() =>
    //    _coroutine = StartCoroutine(Fire());

    private void Update()
    {
        if (transform.position == _targetPoint.ConvertTo<Vector3>() && _coroutine == null)
        {
            _coroutine = StartCoroutine(Fire());
        }
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            _coroutine = null;
        //StopCoroutine(_coroutine);
    }

    private void FixedUpdate() =>
        transform.position = Vector2.MoveTowards(transform.position, _targetPoint, _moveSpeed);

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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Бум");
    }

    public void SetTargetPoint(Vector2 targetPoint) =>
        _targetPoint = targetPoint;

    private IEnumerator Fire()
    {
        WaitForSeconds wait = new(_fireRate);

        while (true)
        {
            Bullet bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
            bullet.SetDirection(Vector3.left);

            yield return wait;
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }
}