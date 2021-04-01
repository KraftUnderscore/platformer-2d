using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    private AudioSource source;
    private Collider2D col;
    private GameObject graphics;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
        graphics = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.IncreaseScore();
            StartCoroutine(Disabler());
        }
    }

    private IEnumerator Disabler()
    {
        col.enabled = false;
        graphics.SetActive(false);
        source.Play();
        yield return new WaitForSeconds(source.clip.length);
        gameObject.SetActive(false);
    }
}
