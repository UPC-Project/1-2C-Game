using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementForce;

    [SerializeField] private Rigidbody2D _rb;
    private PlayerInput _playerInput;
    private Vector2 _input;

    [SerializeField] private GameObject _firingPoint;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }


    private void Update()
    {
        _input = _playerInput.actions["Move"].ReadValue<Vector2>();
        _input = _input.normalized;
        // Firing point control
        if (_input != Vector2.zero)
        {
            float distanceFromPlayer = 0.7f;
            if (_input.x != 0f && _input.y != 0f) distanceFromPlayer = 1f;
            float angle = Mathf.Atan2(_input.y, _input.x) * Mathf.Rad2Deg - 90f;
            Quaternion rot = Quaternion.Euler(0f, 0f, angle);
            _firingPoint.transform.localRotation = rot;
            _firingPoint.transform.localPosition = new Vector3(_input.x, _input.y, 0f) * distanceFromPlayer;
        }
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_input.x * movementForce, _input.y * movementForce);
    }
}
