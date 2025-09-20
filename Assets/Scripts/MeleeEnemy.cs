using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private Transform _attackController;
    [SerializeField] private float _hitRadius;

    protected override void Attack()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(_attackController.position, _hitRadius);
        foreach (Collider2D collider in objects)
        {
            if (collider.CompareTag("Player"))
            {
                collider.transform.GetComponent<Player>().TakeDamage(_damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackController.position, _hitRadius);
    }
}
