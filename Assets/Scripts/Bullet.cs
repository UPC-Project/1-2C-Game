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
        _rb.linearVelocity = Vector2.up * _bulletSpeed;
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // What will happen if the enemy is not ranged? fix
            collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
        }
        gameObject.SetActive(false);

    }


}
