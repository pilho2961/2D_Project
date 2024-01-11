using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shade : MonoBehaviour
{
    public Transform target;
    public GameObject[] skills;
    private Vector2 distanceToTarget;
    Rigidbody2D rb;

    public float maxHp;
    public float currentHp;
    public float speed;
    public bool usingSkill;
    private bool isAnger;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(UsingSkillCondition());
        StartCoroutine(Move());
        isAnger = false;
    }

    void Update()
    {
        target = GameManager.Instance.currentplayer.transform;
        CheckDistanceBetweenTarget();
        LookAtTarget();

        // 거리 확인
        //print(distanceToTarget.magnitude.ToString());
        //print(distanceToTarget.x.ToString());
        //print(distanceToTarget.y.ToString());
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
        while (true)
        {
            skills[2].SetActive(true);
            yield return new WaitForSeconds(2.5f);
            skills[2].SetActive(false);
            yield return new WaitForSeconds(4);
        }
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
        if (!skills[0].activeSelf && !skills[1].activeSelf && !skills[2].activeSelf)
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
            else if (Mathf.Abs(distanceToTarget.x) < 3f && distanceToTarget.y > 0f && distanceToTarget.y < 1f)
            {
                StopCoroutine(UsingFoxSoul());
                StopCoroutine(UsingSohon());
                StartCoroutine(UsingGhostSlap());
            }
            else if (distanceToTarget.magnitude > 4)
            {
                StopCoroutine(UsingSohon());
                StopCoroutine(UsingGhostSlap());
                StartCoroutine(UsingFoxSoul());
            }

            if (currentHp / maxHp < 0.5f && !isAnger)
            {
                isAnger = true;
                StartCoroutine(UsingAnger());
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            if (distanceToTarget.magnitude > 5)
            {
                rb.velocity = distanceToTarget * speed;
                yield return new WaitForSeconds(0.5f);
            }
            else if (distanceToTarget.magnitude < 5)
            {
                rb.velocity = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)) * Random.Range(0.2f, 1f);
                yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            }
        }
    }
}
