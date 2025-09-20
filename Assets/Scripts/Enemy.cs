using UnityEngine;

public abstract class Enemy : HealthSystem
{
    private Rigidbody2D _rb;
    public Transform target;

    [Header("Movement")]
    public float speed;
    public float rotateSpeed = 0.05f;
    public float distanceToStop = 5f;

    [Header("Attack")]
    [SerializeField] protected float _damage;
    [SerializeField] protected float _nextAttackTime;
    [SerializeField] protected float _attackCooldown;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        if (target)
        {
            RotateTowardsTarget();
        }

        if (_nextAttackTime > 0)
        {
            _nextAttackTime -= Time.deltaTime;
        }
        else
        {
            Attack();
            _nextAttackTime = _attackCooldown;
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(target.position, transform.position) >= distanceToStop)
        {
            _rb.linearVelocity = transform.up * speed;
        }
        else
        {
            _rb.linearVelocity = new Vector2(0, 0);
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        float difference = Quaternion.Angle(transform.localRotation, q);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    protected abstract void Attack();
}
