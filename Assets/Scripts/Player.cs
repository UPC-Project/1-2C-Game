using UnityEngine;

public class Player : HealthSystem
{
    // For now is the same damage if is it ranged or melee, see preferences.
    [SerializeField] private float _attackDamage;

    [Header("Melee Attack")]
    [SerializeField] private float _nextMeleeAttackTime;
    [SerializeField] private float _attackMeleeCooldown;
    [SerializeField] private float _hitRadius;

    [Header("Ranged Attack")]
    [SerializeField] private float _nextRangedAttackTime;
    [SerializeField] private float _attackRangedCooldown;
    [SerializeField] private GameObject _firingPoint;


    private void Update()
    {
        if (_nextMeleeAttackTime > 0)
        {
            _nextMeleeAttackTime -= Time.deltaTime;
        }
        if (_nextRangedAttackTime > 0)
        {
            _nextRangedAttackTime -= Time.deltaTime;
        }
    }

    public void OnAttack()
    {
        if (_nextMeleeAttackTime <= 0)
        {
            Attack();
            _nextMeleeAttackTime = _attackMeleeCooldown;
        }
    }

    public void OnRangedAttack()
    {
        if (_nextRangedAttackTime <= 0)
        {
            RangedAttack();
            _nextRangedAttackTime = _attackRangedCooldown;
        }
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
        GameObject bullet = BulletPool.Instance.RequestBullet(_firingPoint.transform.position, _firingPoint.transform.rotation);
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
