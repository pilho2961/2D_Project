using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearSkill : MonoBehaviour
{
    Animator animator;
    public int damage;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    public void ActivateSkill()
    {
        animator.SetBool("isActive", true);
    }

    public void DeactivateSkill()
    {
        animator.SetBool("isActive", false);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<Player>() != null)
            {
                other.GetComponent<Player>().TakeDamage(damage);
            }
            else if (other.GetComponent<Player>() == null)
            {
                other.GetComponent<Metamorphosis>().TakeDamage(damage);
            }
        }
    }
}
