using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anger : MonoBehaviour
{
    Animator animator;
    public int damage;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        Vector2 randPos = new Vector2(Random.Range(-4, 4), Random.Range(-3, 3));
        Vector3 randRot = new Vector3(0, 0, Random.Range(0f, 360f));
        transform.position = randPos;
        transform.Rotate(randRot);
    }

    public void ActivateSkill()
    {
        animator.SetBool("isActive", true);
    }

    public void DeactivateSkill()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
