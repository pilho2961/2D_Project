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
    }

    public void ActivateSkill()
    {
        animator.SetBool("isActive", true);
    }

    public void DeactivateSkill()
    {
        Destroy(gameObject);
    }
}
