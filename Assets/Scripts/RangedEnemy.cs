using UnityEngine;

// TODO: Apply Pooling to bullets
public class RangedEnemy : Enemy
{
    public float distanceToShoot = 8f;
    [SerializeField] private GameObject _firingPoint;

    protected override void Attack()
    {
        if (Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            GameObject bullet = BulletPool.Instance.RequestBullet(_firingPoint.transform.position, transform.rotation);
        }
    }
}
