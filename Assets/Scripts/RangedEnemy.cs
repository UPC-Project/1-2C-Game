using UnityEngine;

// TODO: Apply Pooling to bullets
public class RangedEnemy : HealthSystem
{
    [SerializeField] private float _damage;
    private Rigidbody2D _rb;
    public Transform target;

    [Header("Movement and Distances")]
    public float speed;
    public float rotateSpeed = 0.0025f;
    public float distanceToShoot = 8f;
    public float distanceToStop = 5f;

    [Header("Shoot control")]
    public float fireRate;
    private float _timeToFire = 0f;
    public Transform firingPoint;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (health <= 0) Death();
        if (Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            Shoot();
        }
        if (target)
        {
            RotateTowardsTarget();
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
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void Shoot()
    {
        if (_timeToFire <= 0)
        {
            Debug.Log("shoot");
            _timeToFire = fireRate;
        }
        else
        {
            _timeToFire -= Time.deltaTime;
        }
    }
    private void Death()
    {
        Destroy(gameObject);
    }

}
