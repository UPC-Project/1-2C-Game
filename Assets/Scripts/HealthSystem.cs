using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    public float health;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
            GetComponent<Player>().Respawn();
        }
    }

    // if necessary, override this method.
    public virtual void Death()
    {
        Destroy(gameObject);
    }

}
