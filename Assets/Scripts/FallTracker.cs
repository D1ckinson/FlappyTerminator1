using UnityEngine;

public class FallTracker : MonoBehaviour
{
    [SerializeField] private Spaceship _spaceship;

    private float _yMinusDeadZone;

    private void Awake()
    {
        Vector3 cameraToObject = _spaceship.transform.position - Camera.main.transform.position;
        float distance = Vector3.Project(cameraToObject, Camera.main.transform.forward).z;
        Vector3 leftBot = Camera.main.ViewportToWorldPoint(new(0, 0, distance));

        _yMinusDeadZone = leftBot.y;
    }

    private void Update()
    {
        if (_spaceship == null)
            return;

        if (_spaceship.transform.position.y < _yMinusDeadZone)
            Destroy(_spaceship.gameObject);
    }
}
