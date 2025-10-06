using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _shotPrefab;
    [SerializeField] private Transform _attackController;   
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _shotPoint;


    public float health;

    [SerializeField] private float _hitRadius;
    [SerializeField] private float _attackDamage;

    [SerializeField] private float _nexAttacKTime;
    [SerializeField] private float _attackCooldown;


    [SerializeField] private float _bulletOffset = 0.5f;



    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();


    }

    private void Update()
    {
        if (_nexAttacKTime > 0)
        {
            _nexAttacKTime -= Time.deltaTime;
             
        }
        if (_playerInput.actions["Attack"].triggered && _animator.GetFloat("Speed") == 0 && _nexAttacKTime <= 0)
        {
            Attack();
            _animator.SetBool("isAttackingMeele", true);
            _nexAttacKTime = _attackCooldown;
            Debug.Log("Input Meele Attack");
        }
        else
        {
            _animator.SetBool("isAttackingMeele", false); 
        }

        if (_playerInput.actions["RangedAttack"].triggered && _animator.GetFloat("Speed") == 0 && _nexAttacKTime <= 0)
        {
            RangedAttack();
            _animator.SetBool("isAttackingRanged", true);
            _nexAttacKTime = _attackCooldown;
            Debug.Log("Input Ranged Attack");
        }
        else
        {
            _animator.SetBool("isAttackingRanged", false);
        }


    }

    private void Attack()
    {
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

    private void RangedAttack()
    {
        GameObject bullet = BulletPool.Instance.RequestBullet();
        float direction = transform.localScale.x > 0 ? 1f : -1f;
        Vector3 spawnPos = _shotPoint.position;
        bullet.transform.position = spawnPos;
        bullet.GetComponent<Bullet>().SetDirection(direction);
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
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackController.position, _hitRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_shotPoint.position, 0.1f);
    }



}
