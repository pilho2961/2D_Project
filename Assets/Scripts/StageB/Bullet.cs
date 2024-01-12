using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public bool isMelee;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grass")
        {
            Destroy(gameObject, 3f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (!isMelee && other.gameObject.tag == "Wall")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
