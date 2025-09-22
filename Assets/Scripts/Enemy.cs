using UnityEngine;

public abstract class Enemy : HealthSystem
{
    private Rigidbody2D _rb;
    public Transform target;
    [SerializeField] private float _aggroRadius;
    private bool onAggro = false;

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
        onAggro = false;
    }

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        if (target && onAggro)
        {
            RotateTowardsTarget();

            if (_nextAttackTime > 0)
            {
                _nextAttackTime -= Time.deltaTime;
            }
            else
            {
                Attack();
                _nextAttackTime = _attackCooldown;
            }
        } else
        {
            Collider2D[] objects = Physics2D.OverlapCircleAll(gameObject.transform.position, _aggroRadius);
            foreach (Collider2D collider in objects)
            {
                if (collider.CompareTag("Player"))
                {
                    onAggro = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(target.position, transform.position) >= distanceToStop && onAggro)
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

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, _aggroRadius);
    }
}
