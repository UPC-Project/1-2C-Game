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
        angle = fixAngle(angle);
        Quaternion rot = Quaternion.Euler(0f, 0f, angle - 90f);
        _facingPoint.transform.localRotation = rot;
        _facingPoint.transform.localPosition = Quaternion.Euler(0, 0, angle) * new Vector3(distanceFromPlayer, 0, 0);
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_input.x * movementForce, _input.y * movementForce);
    }

    // The animation will be only in 8 angles
    private float fixAngle(float angle)
    {
        if (Mathf.Abs(angle) <= 22.5)
        {
            return 0f;
        }
        else if (Mathf.Abs(angle) <= 67.5)
        {
            return 45f * Mathf.Sign(angle);
        }
        else if (Mathf.Abs(angle) <= 112.5)
        {
            return 90f * Mathf.Sign(angle);
        }
        else if (Mathf.Abs(angle) <= 157.5)
        {
            return 135f * Mathf.Sign(angle);
        }
        else
        {
            return 180f;
        }
    }
}
