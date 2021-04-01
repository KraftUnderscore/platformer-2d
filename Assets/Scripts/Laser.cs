using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float shootingSpeed;

    private Animator anim;

    private bool isShooting;
    private bool isReloading;

    private void Awake()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isShooting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isShooting = false;
        }
    }

    private void FixedUpdate()
    {
        if(isShooting)
            Shoot();
    }

    private void Shoot()
    {
        if (isReloading) return;

        anim.SetTrigger("shoot");
        SpawnBullet();
        StartCoroutine(Reload());
    }

    private void SpawnBullet()
    {
        GameObject bullet = BulletPool.instance.GetBullet();
        bullet.transform.rotation = transform.rotation;
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(1f / shootingSpeed);
        isReloading = false;
    }
}
