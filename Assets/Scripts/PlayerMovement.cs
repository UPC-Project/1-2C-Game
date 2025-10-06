using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementForce;

    [SerializeField] private Rigidbody2D _rb;
    private PlayerInput _playerInput;
    private Vector2 _input;

    [SerializeField] private GameObject _facingPoint;
    private float distanceFromPlayer = 1f;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }


    private void Update()
    {
        // Movement
        _input = _playerInput.actions["Move"].ReadValue<Vector2>();
        _input = _input.normalized;

        // Aiming with mouse
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        Quaternion rot = Quaternion.Euler(0f, 0f, angle - 90f);
        _facingPoint.transform.localRotation = rot;
        _facingPoint.transform.localPosition = Quaternion.Euler(0, 0, angle) * new Vector3(distanceFromPlayer,0,0);
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_input.x * movementForce, _input.y * movementForce);
    }
}
