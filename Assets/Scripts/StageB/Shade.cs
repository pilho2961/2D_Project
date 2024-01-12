using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shade : MonoBehaviour
{
    public Transform target;
    public GameObject[] skills;
    private Vector2 distanceToTarget;
    Rigidbody2D rb;

    public float maxHp = 100f;
    public float currentHp;
    public ShadeHpBarController hpBar;
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

    private void Start()
    {
        currentHp = maxHp;
    }

    void Update()
    {
        target = GameManager.Instance.currentplayer.transform;
        CheckDistanceBetweenTarget();
        LookAtTarget();
        hpBar.SetHealth(currentHp, maxHp);
        if (currentHp == 0f)
        {
            Die();
        }

        // 거리 확인
        //print(distanceToTarget.magnitude.ToString());
        //print(distanceToTarget.x.ToString());
        //print(distanceToTarget.y.ToString());
    }

    private void UsingSohon()
    {
        skills[0].SetActive(true);
    }

    private void OffSohon()
    {
        skills[0].SetActive(false);
    }

    private void UsingGhostSlap()
    {
        skills[1].SetActive(true);
    }
    private void OffGhostSlap()
    {
        skills[1].SetActive(false);
    }

    private void UsingFoxSoul()
    {
        Instantiate(skills[3],transform);
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
                UsingSohon();
                yield return new WaitForSeconds(4);
                OffSohon();
            }
            else if (Mathf.Abs(distanceToTarget.x) < 3f && distanceToTarget.y > -1f && distanceToTarget.y < 1f)
            {
                UsingGhostSlap();
                yield return new WaitForSeconds(2);
                OffGhostSlap();
            }
            else if (distanceToTarget.magnitude > 4)
            {
                UsingFoxSoul();
                yield return new WaitForSeconds(1);

            }

            if (currentHp / maxHp < 0.5f && !isAnger)
            {
                isAnger = true;
                StartCoroutine(UsingAnger());
            }
            yield return null;
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
                rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * Random.Range(0.3f, 1.2f);
                yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (currentHp > damage)
        {
            currentHp -= damage;
        }
        if (currentHp <= damage)
        {
            currentHp = 0f;
        }
    }

    private void Die()
    {
        //TODO:shade 비활성화 후 게임 진행
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSkill"))
        {
            // TODO:스킬의 데미지에 따라 보스몬스터의 체력 감소(TakeHit())
            
        }
    }
}
