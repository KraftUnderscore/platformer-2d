using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [HideInInspector] public static BulletPool instance;

    [SerializeField] private GameObject bulletPrefab;

    private List<GameObject> bulletPool;

    private void Awake()
    {
        instance = this;
        bulletPool = new List<GameObject>();
    }

    public GameObject GetBullet()
    {
        foreach(GameObject obj in bulletPool)
            if (!obj.activeSelf) return obj;

        return CreateBullet();
    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform);
        bullet.SetActive(false);
        bulletPool.Add(bullet);
        return bullet;
    }
}
