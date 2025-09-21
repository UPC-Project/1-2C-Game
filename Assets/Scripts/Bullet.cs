using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private float _lifeTime = 0.5f;
    private float _lifeTimer;


    private void OnEnable()
    {
        _rb.linearVelocity = transform.up * _bulletSpeed;
        _lifeTimer = _lifeTime;
    }

    private void Update()
    {
        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: if the bullet is from the player it shouldn't hit them.

        // Bullet shoot from player to enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
        }

        // Bullet shoot from enemy to player
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(_damage);
        }
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }


}
