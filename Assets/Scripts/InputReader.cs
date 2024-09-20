using UnityEngine;

[RequireComponent(typeof(Jump))]
public class InputReader : MonoBehaviour
{
    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;

    private Jump _jump;

    private void Awake() => 
        _jump = GetComponent<Jump>();

    private void Update()
    {
        if (Input.GetKeyDown(_jumpButton))
            _jump.IsHop = true;
    }
}
