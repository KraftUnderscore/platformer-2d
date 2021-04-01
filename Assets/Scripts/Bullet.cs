using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float disableTime;

    [HideInInspector] public Vector2 direction;

    private Rigidbody2D rigid;
    private Collider2D col;
    private GameObject graphics;
    private GameObject splash;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        graphics = transform.GetChild(0).gameObject;
        splash = transform.GetChild(1).gameObject;
    }

    private void OnEnable()
    {
        col.enabled = true;
        graphics.SetActive(true);
        splash.SetActive(false);
        rigid.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("laser") || collision.CompareTag("coin")) return;
        if (collision.CompareTag("Player"))
        {
            PlayerController.instance.GetDamage();
        }
        Stop();
    }

    private void Stop()
    {
        rigid.velocity = Vector2.zero;
        col.enabled = false;
        graphics.SetActive(false);
        splash.SetActive(true);
        StartCoroutine(DisableTimer());
    }

    private IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(disableTime);
        gameObject.SetActive(false);
    }
}
