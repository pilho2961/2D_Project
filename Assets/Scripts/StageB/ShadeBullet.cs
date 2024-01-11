using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeBullet : Bullet
{
    public Transform target;
    public Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.Instance.currentplayer.transform;
        Shot();
    }

    private void Shot()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.AddForce(direction * 10f, ForceMode2D.Impulse);
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
