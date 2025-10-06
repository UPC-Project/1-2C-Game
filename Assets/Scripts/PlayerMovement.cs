using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementForce;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    private PlayerInput _playerInput;
    private Vector2 _input;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();


    }


    private void Update()
    {
        _input = _playerInput.actions["Move"].ReadValue<Vector2>();
        _input = _input.normalized;

        if (_input.x != 0 || _input.y != 0)
        {
            _animator.SetFloat("H", _input.x);
            _animator.SetFloat("Y", _input.y);
            _animator.SetFloat("Speed", 1);

        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }

        //if (_playerInput.actions["Attack"].triggered) _animator.SetBool("isAttacking2", true);
        //else _animator.SetBool("isAttacking2", false);

        //if (_playerInput.actions["RangedAttack"].triggered) _animator.SetBool("isAttacking", true);
        //else _animator.SetBool("isAttacking", false);



    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_input.x * movementForce, _input.y * movementForce);


    }
}
