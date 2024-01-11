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
}
