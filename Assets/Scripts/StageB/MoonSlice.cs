using System.Collections;
using System.Collections.Generic;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Shade>().TakeDamage(damage);
            gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grass")
        {
            gameObject.SetActive(false);
        }
    }
}
