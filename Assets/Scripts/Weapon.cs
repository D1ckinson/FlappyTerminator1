using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private float _divider = 2;
    private float _halfSize;
    private Vector3 _direction;

    private void OnEnable() =>
        _halfSize = GetComponent<SpriteRenderer>().bounds.size.x / _divider + 1;

    public void SetDirection(bool isShootToRight)
    {
        if (isShootToRight)
        {
            _direction = Vector3.right;
        }
        else
        {
            _halfSize *= -1;
            _direction = Vector3.left;
        }
    }

    public void Fire()
    {
        Vector3 firePoint = new(_halfSize, 0, 0);
        firePoint += transform.position;

        Bullet bullet = Instantiate(_bullet, firePoint, Quaternion.identity);
        bullet.SetDirection(_direction);
    }
}
