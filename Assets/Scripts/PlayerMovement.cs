using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementForce;

    [SerializeField] private Rigidbody2D _rb;
    private PlayerInput _playerInput;
    private Vector2 _input;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }


    private void Update()
    {
        _input = _playerInput.actions["Move"].ReadValue<Vector2>();
        _input = _input.normalized;
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_input.x * movementForce, _input.y * movementForce);
    }
}
