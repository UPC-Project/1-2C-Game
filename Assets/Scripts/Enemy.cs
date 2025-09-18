using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 20;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
        Debug.Log("EL enemigo murio");
    }

}