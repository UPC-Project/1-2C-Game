using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _damage = 5f;

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private float _lifeTime = 0.2f; 
    private float _lifeTimer;
    private float direction = 1f;


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
            collision.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
            //Llamar al meto "Recibir daño" correspondiente]
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Environment"))
        {
            gameObject.SetActive(false);
        }

    }

    public void SetDirection(float dir)
    {
        direction = dir;
    }


}
