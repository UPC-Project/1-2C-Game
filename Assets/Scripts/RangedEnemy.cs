using UnityEngine;

// TODO: Apply Pooling to bullets
public class RangedEnemy : Enemy
{
    [Header("Shoot control")]
    public float distanceToShoot = 8f;
    public Transform firingPoint;

    protected override void Attack()
    {
        if (Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            Debug.Log("shoot");
        }
    }

 
}
