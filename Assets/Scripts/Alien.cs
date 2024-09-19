using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] private float _fireRate = 5f;
    [SerializeField] private float _moveSpeed = 0.07f;
    [SerializeField] private DamageHandler _damageHandler;
    [SerializeField] private Weapon _weapon;

    private Vector2 _targetPoint;
    private Coroutine _coroutine;
    private Action _disable;

    private void Start()
    {
        _damageHandler.SetAction(_disable);
        _weapon.SetDirection(false);
    }

    private void Update()
    {
        if (transform.position == _targetPoint.ConvertTo<Vector3>() && _coroutine == null)
            _coroutine = StartCoroutine(Fire());

        transform.position = Vector2.MoveTowards(transform.position, _targetPoint, _moveSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            _coroutine = null;
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
            _weapon.Fire();

            yield return wait;
        }
    }
}