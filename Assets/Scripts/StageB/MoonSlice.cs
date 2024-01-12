using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class MoonSlice : MonoBehaviour
{
    public float damage;
    public float speed;
    Rigidbody2D rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DeActive());
    }

    void Update()
    {
        if (rb.transform.localScale.x == 0.3f)
        {
            rb.velocity = transform.up * speed;
        }
        else if (rb.transform.localScale.x == 1f)
        {
            rb.velocity = transform.up * speed * 3f;
            damage = 15f;
        }
    }

    private IEnumerator DeActive()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Shade>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
