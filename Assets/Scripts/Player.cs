using System.Collections.ObjectModel;
using UnityEngine;

public class Player : MonoBehaviour
{

    //[SerializeField] private GameObject _shotPrefab;
    [SerializeField] private Transform _attackController;

    public float health;

    [SerializeField] private float  _hitRadius;
    [SerializeField] private float _attackDamage;

    [SerializeField] private float _nexAttacKTime;
    [SerializeField] private float _attackCooldown;


    [SerializeField] private float _bulletOffset = 0.5f;



    private void Update()
    {
        if (_nexAttacKTime > 0)
        {
            _nexAttacKTime -= Time.deltaTime;
             
        }
        if (Input.GetKeyDown("z") && _nexAttacKTime <= 0)
        {
            Attack();
            _nexAttacKTime = _attackCooldown;
        }
        if (Input.GetButtonDown("Fire1") && _nexAttacKTime <= 0)
        {
            GameObject bullet = BulletPool.Instance.RequestBullet();
            bullet.transform.position = transform.position + Vector3.up * _bulletOffset;
            _nexAttacKTime = _attackCooldown;

        }
    }
    private void Attack()
    {
        //animator.SetTrigger("");     Animacison correspondiente 
        Collider2D[] objects = Physics2D.OverlapCircleAll(_attackController.position, _hitRadius);
        foreach (Collider2D collider in objects)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.transform.GetComponent<Enemy>().TakeDamage(_attackDamage);
                //Llamar al meto "Recibir daño" correspondiente
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackController.position, _hitRadius);
    }

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
        Debug.Log("haz muerto");
    }

}
