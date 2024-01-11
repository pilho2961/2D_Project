using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Metamorphosis : MonoBehaviour
{
    public bool canMove = true;
    public float speed;
    public Transform aim;
    Animator animator;
    public float teleportDistance;
    private float teleportCoolTime = 0f;
    Rigidbody2D rb;

    public float metamorphosisGage;

    private float maxHp;
    [SerializeField]
    private float currentHp;
    Slider hpBar;
    TextMeshProUGUI hpText;
    public float GetHpValue() { return currentHp / maxHp; }

    public ShadeBullet shadeBullet;
    public Anger anger;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hpBar = GetComponentInChildren<Slider>();
        hpText = GetComponentInChildren<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        maxHp = 150f;
        currentHp = 150f;
    }

    void Update()
    {
        Move();
        Aim();
        HpBarChange();
        ComeBack();
    }

    private void Move()
    {
        if (canMove)
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
            }

            dir.Normalize();
            GetComponent<Rigidbody2D>().velocity = speed * dir;

            if (SkillManager.Instance.IsSkillReady(teleportCoolTime))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GetComponent<CapsuleCollider2D>().enabled = false;  // 순간이동 시 충돌판정 제거
                    rb.MovePosition(rb.position + dir * teleportDistance);
                    //TODO: 순간이동 애니메이션 추가
                    teleportCoolTime = Time.time + 0.5f;
                }
            }

            GetComponent<CapsuleCollider2D>().enabled = true;
            animator.SetBool("isFlying", rb.velocity != Vector2.zero);
        }
    }

    private void Aim()
    {
        Vector2 mousePositionScreen = Input.mousePosition;
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(new Vector2(mousePositionScreen.x, mousePositionScreen.y));

        Vector2 direction = mousePositionWorld - (Vector2)transform.position;
        if (direction.x > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else if (direction.x < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }

        Vector2 aimPosition = direction.normalized * 3.5f;

        aim.transform.position = new Vector2(transform.position.x + aimPosition.x, transform.position.y + aimPosition.y);
    }

    private void OnEnable()
    {
        metamorphosisGage = 100;
        StartCoroutine(LosemetamorphosisGage());
    }

    private IEnumerator LosemetamorphosisGage()
    {
        while (metamorphosisGage > 0)
        {
            metamorphosisGage -= 5;
            yield return new WaitForSeconds(1);
        }
    }

    private void ComeBack()
    {
        if (metamorphosisGage <= 0)
        {
            GameManager.Instance.TurnToNormal();
        }
    }

    public void HpBarChange()
    {
        hpBar.value = GetHpValue();
        hpText.text = currentHp.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(shadeBullet.damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(anger.damage);
        }
    }

    private void TakeDamage(float damage)
    {
        currentHp -= damage;
    }
}
