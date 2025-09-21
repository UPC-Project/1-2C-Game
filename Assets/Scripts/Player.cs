using UnityEngine;

public class Player : HealthSystem
{
    [SerializeField] private GameObject _firingPoint;
    [SerializeField] private float _hitRadius;
    [SerializeField] private float _attackDamage;

    [SerializeField] private float _nextAttackTime;
    [SerializeField] private float _attackCooldown;

    [SerializeField] private float _bulletOffset = 0.5f;

    private void Update()
    {
        if (_nextAttackTime > 0)
        {
            _nextAttackTime -= Time.deltaTime;
        }
    }

    public void OnAttack()
    {
        if (_nextAttackTime <= 0)
        {
            Attack();
            _nextAttackTime = _attackCooldown;
        }
    }

    public void OnRangedAttack()
    {
        RangedAttack();
    }
    private void Attack()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(gameObject.transform.position, _hitRadius);
        foreach (Collider2D collider in objects)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.transform.GetComponent<Enemy>().TakeDamage(_attackDamage);
            }
        }
    }

    private void RangedAttack()
    {
        if (_nextAttackTime <= 0)
        {
            GameObject bullet = BulletPool.Instance.RequestBullet(_firingPoint.transform.position, _firingPoint.transform.rotation);
            _nextAttackTime = _attackCooldown;
        }
    }

    public override void Death()
    {
        Debug.Log("You died");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, _hitRadius);
    }
}
