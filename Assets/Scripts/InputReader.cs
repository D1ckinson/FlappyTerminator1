using UnityEngine;

[RequireComponent(typeof(Mover))]
public class InputReader : MonoBehaviour
{
    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;

    private Mover _mover;

    void Start() => 
        _mover = GetComponent<Mover>();

    void Update()
    {
        if (Input.GetKeyDown(_jumpButton))
            _mover.IsJump = true;
    }
}
