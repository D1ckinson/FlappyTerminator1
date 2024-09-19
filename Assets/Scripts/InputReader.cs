using UnityEngine;

[RequireComponent(typeof(Jump))]
public class InputReader : MonoBehaviour
{
    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;

    private Jump _mover;

    void Start() => 
        _mover = GetComponent<Jump>();

    void Update()
    {
        if (Input.GetKeyDown(_jumpButton))
            _mover.IsJump = true;
    }
}
