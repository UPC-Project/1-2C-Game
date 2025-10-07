using UnityEngine;

public abstract class HealthSystem : MonoBehaviour
{
    [Header("Health")]
    public int health;
    public int maxHealth = 5;

    public void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
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
