using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class BulletPool : MonoBehaviour
{

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private List<GameObject> _bulletList;
    private int _poolSize = 2; 

    private static BulletPool instance;
    public static BulletPool Instance { get { return instance; } }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        AddBulletsToPool(_poolSize);
    }

    private void AddBulletsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.SetActive(false);
            _bulletList.Add(bullet);
            bullet.transform.parent = transform;
        }
    }

    public GameObject RequestBullet()
    {
        for (int i = 0; i < _bulletList.Count; i++)
        {
            if (!_bulletList[i].activeSelf)
            {
                _bulletList[i].SetActive(true);
                return _bulletList[i];
            }
        }
        AddBulletsToPool(1);
        _bulletList[_bulletList.Count - 1].SetActive(true);
        return _bulletList[_bulletList.Count - 1];
    }
}