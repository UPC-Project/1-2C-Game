using UnityEngine;


public class Player : HealthSystem
{
    [SerializeField] private Transform _attackController;
    [SerializeField] private float  _hitRadius;
    [SerializeField] private float _attackDamage;

    [SerializeField] private float _nexAttacKTime;
    [SerializeField] private float _attackCooldown;

    [SerializeField] private float _bulletOffset = 0.5f;

    private void Update()
    {
        if (_nexAttacKTime > 0)
        {
            _nexAttacKTime -= Time.deltaTime;
        }
    }

    public void OnAttack()
    {
        if (_nexAttacKTime <= 0)
        {
            Attack();
            _nexAttacKTime = _attackCooldown;
        }
    }

    public void OnRangedAttack()
    {
        RangedAttack();

    }
    private void Attack()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(_attackController.position, _hitRadius);
        foreach (Collider2D collider in objects)
        {
            if (collider.CompareTag("Enemy"))
            {
                // What will happen if the enemy is not ranged? fix
                collider.transform.GetComponent<Enemy>().TakeDamage(_attackDamage);
            }
        }
    }

    private void RangedAttack()
    {
        if (_nexAttacKTime <= 0)
        {
            GameObject bullet = BulletPool.Instance.RequestBullet();
            bullet.transform.position = transform.position + Vector3.up * _bulletOffset;
            _nexAttacKTime = _attackCooldown;

        }
    }

    public override void Death()
    {
        Debug.Log("You died");  
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackController.position, _hitRadius);
    }
}
