using System.Collections;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 5f;
    [SerializeField] private int _alienCount = 5;
    [SerializeField] private Alien _alien;

    private Pool<Alien> _alienPool;
    private Vector2 _spawnPoint1;
    private Vector2 _spawnPoint2;
    private Vector2 _targetPoint1;
    private Vector2 _targetPoint2;
    private float _ySpawnOffset = -2f;
    private float _xTargetPointOffset = 1f;
    private float _yTargetPointOffset = 1f;

    private void Start()
    {
        _alienPool = new(PreloadAlienFunc, GetAlienAction, ReturnAlienAction, _alienCount);

        Vector3 cameraToObject = transform.position - Camera.main.transform.position;
        float distance = Vector3.Project(cameraToObject, Camera.main.transform.forward).z;

        Vector3 rightBot = Camera.main.ViewportToWorldPoint(new(1, 0, distance));
        Vector3 rightTop = Camera.main.ViewportToWorldPoint(new(1, 1, distance));

        _spawnPoint1 = new(rightTop.x, rightTop.y - _ySpawnOffset);
        _spawnPoint2 = new(rightBot.x, rightBot.y - _ySpawnOffset);
        _targetPoint1 = new(rightTop.x - _xTargetPointOffset, rightTop.y - _yTargetPointOffset);
        _targetPoint2 = new(rightBot.x - _xTargetPointOffset, rightBot.y + _yTargetPointOffset);

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new(_spawnTime);

        while (true)
        {
            if (_alienPool.CurrentSpawns < _alienCount)
                _alienPool.Get();

            yield return wait;
        }
    }

    private Vector2 GetSpawnPoint()
    {
        float x = Random.Range(_spawnPoint1.x, _spawnPoint2.x);
        float y = Random.Range(_spawnPoint1.y, _spawnPoint2.y);

        return new Vector2(x, y);
    }

    private Vector2 GetTargetPoint()
    {
        float x = _targetPoint1.x;
        float y = Random.Range(_targetPoint1.y, _targetPoint2.y);

        return new Vector2(x, y);
    }

    private Alien PreloadAlienFunc()
    {
        Alien alien = Instantiate(_alien);
        alien.SetDisableAction(() => _alienPool.Return(alien));

        return alien;
    }

    private void GetAlienAction(Alien alien)
    {
        alien.transform.position = GetSpawnPoint();
        alien.SetTargetPoint(GetTargetPoint());
        alien.gameObject.SetActive(true);
    }

    private void ReturnAlienAction(Alien alien) =>
        alien.gameObject.SetActive(false);
}
