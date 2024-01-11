using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shade : MonoBehaviour
{
    public Transform target;
    public GameObject[] skills;
    private Vector2 distanceToTarget;

    public float maxHp;
    public float currentHp;
    public float speed;
    public bool usingSkill;

    private void Awake()
    {
        StartCoroutine(UsingSkillCondition());
    }

    void Update()
    {
        target = GameManager.Instance.currentplayer.transform;
        CheckDistanceBetweenTarget();
        LookAtTarget();

        print(distanceToTarget.magnitude.ToString());
        print(distanceToTarget.x.ToString());
        print(distanceToTarget.y.ToString());
    }

    private IEnumerator UsingSohon()
    {
        skills[0].SetActive(true);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator UsingGhostSlap()
    {
        skills[1].SetActive(true);
        yield return new WaitForSeconds(1);

    }

    private IEnumerator UsingAnger()
    {
        skills[2].SetActive(true);
        yield return new WaitForSeconds(5);
        skills[2].SetActive(false);

    }

    private IEnumerator UsingFoxSoul()
    {
        Instantiate(skills[3], transform);
        yield return new WaitForSeconds(1);

    }

    private void CheckDistanceBetweenTarget()
    {
        distanceToTarget = target.position - transform.position;
    }

    private void LookAtTarget()
    {
        if (distanceToTarget.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (distanceToTarget.x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private IEnumerator UsingSkillCondition()
    {
        while(true)
        {
            if (Mathf.Abs(distanceToTarget.x) < 2.5f && distanceToTarget.y > 1f && distanceToTarget.y < 1.8f)
            {
                StopCoroutine(UsingFoxSoul());
                StopCoroutine(UsingGhostSlap());
                StartCoroutine(UsingSohon());
            }
            else if (Mathf.Abs(distanceToTarget.x) < 2.5f && distanceToTarget.y > 0f && distanceToTarget.y < 1f)
            {
                StopCoroutine(UsingFoxSoul());
                StopCoroutine(UsingSohon());
                StartCoroutine(UsingGhostSlap());
            }
            else if (distanceToTarget.magnitude > 5)
            {
                StopCoroutine(UsingSohon());
                StopCoroutine(UsingGhostSlap());
                StartCoroutine(UsingFoxSoul());
            }

            if (currentHp / maxHp < 0.5f)
            {
                StartCoroutine(UsingAnger());
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
