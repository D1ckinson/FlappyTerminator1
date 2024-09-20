using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    private float _divider = 2;
    private float _halfSize;
    private int _bulletCount = 1;
    private Vector3 _direction;
    private Pool<Bullet> _pool;

    private void Awake()
    {
        _halfSize = GetComponent<SpriteRenderer>().bounds.size.x / _divider + 1;
        _pool = new(PreloadBulletFunc, GetBulletAction, ReturnBulletAction, _bulletCount);
    }

    public void SetDirection(bool isShootToRight)
    {
        if (isShootToRight)
        {
            _halfSize = Mathf.Abs(_halfSize);
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
        Bullet bullet = _pool.Get();

        bullet.SetDirection(_direction);
    }

    private Bullet PreloadBulletFunc()
    {
        Bullet bullet = Instantiate(_bulletPrefab);
        bullet.SetDisableAction(() => _pool.Return(bullet));

        return bullet;
    }

    private void GetBulletAction(Bullet bullet)
    {
        bullet.transform.position = GetFirePoint();
        bullet.gameObject.SetActive(true);
    }

    private void ReturnBulletAction(Bullet bullet) =>
        bullet.gameObject.SetActive(false);

    private Vector3 GetFirePoint()
    {
        Vector3 firePoint = new(_halfSize, 0, 0);
        firePoint += transform.position;

        return firePoint;
    }
}
