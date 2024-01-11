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
        StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        rb.AddForce(target.position * 1f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
        StopCoroutine(Shot());
    }


}
