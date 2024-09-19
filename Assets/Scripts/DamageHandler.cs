using System;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    private Action _action;

    public void SetAction(Action action) =>
        _action = action;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponentInChildren<Bullet>();

        if (bullet == null)
            return;

        _action?.Invoke();
    }
}
