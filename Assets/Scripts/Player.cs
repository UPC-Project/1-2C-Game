using UnityEngine;

public class Player : HealthSystem
{
    public int healingPotions = 0;

    [Header("Melee Attack")]
    [SerializeField] private int _meleeAttackDamage = 1;
    [SerializeField] private float _nextMeleeAttackTime;
    [SerializeField] private float _attackMeleeCooldown;
    [SerializeField] private float _hitRadius;

    [Header("Ranged Attack")]
    [SerializeField] private float _nextRangedAttackTime;
    [SerializeField] private float _attackRangedCooldown;
    [SerializeField] private GameObject _facingPoint;


    //CheckPoint System 
    private Vector2 _respawnPoint;

    void Start()
    {



        _respawnPoint = transform.position;
    }

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

    // ATTACK SYSTEM
    // Triggered when Z key is pressed
    public void OnMeleeAttack()
    {
        if (_nextMeleeAttackTime <= 0)
        {
            MeleeAttack();
            _nextMeleeAttackTime = _attackMeleeCooldown;
        }
        Debug.Log("Melee Attack");
    }

    // Triggered when X key is pressed
    public void OnRangedAttack()
    {
        if (_nextRangedAttackTime <= 0)
        {
            RangedAttack();
            _nextRangedAttackTime = _attackRangedCooldown;
        }
        Debug.Log("Ranged Attack");

    }

    private void MeleeAttack()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(gameObject.transform.position, _hitRadius);
        foreach (Collider2D collider in objects)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.transform.GetComponent<Enemy>().TakeDamage(_meleeAttackDamage);
            }
        }
    }

    private void RangedAttack()
    {
        // The bullet damage is in the Bullet script
        GameObject bullet = BulletPool.Instance.RequestBullet(_facingPoint.transform.position, _facingPoint.transform.rotation);
    }

    public override void Death()
    {
        Debug.Log("You died");
    }


    // Comment this function if you don't want to see the melee range attack
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, _hitRadius);
    }

    public void SetCheckpoint(Vector2 newPosition)
    {
        _respawnPoint = newPosition;
    }

    public void Respawn()
    {
        transform.position = _respawnPoint;
    }
    
    // HEAL SYSTEM
    // Triggered when H key is pressed
    public void OnHeal()
    {
        if (healingPotions > 0)
        {
            health = maxHealth;
            healingPotions -= 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealingPotion"))
        {
            healingPotions += 1;
            Destroy(collision.gameObject);
        }
    }

}
