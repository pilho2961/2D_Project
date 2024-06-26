using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStageA : MonoBehaviour
{
    public bool canMove = true;
    public float speed;
    public Transform aim;
    Animator animator;
    public float teleportDistance;
    private float teleportCoolTime = 0f;
    Rigidbody2D rb;

    private float maxHp;
    [SerializeField]
    public float currentHp;
    public float GetHpValue() { return currentHp / maxHp; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        maxHp = 100f;
        currentHp = 100f;
    }

    private void Update()
    {
        Move();
        Aim();
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
            rb.velocity = speed * dir;

            if (SkillManager.Instance.IsSkillReady(teleportCoolTime))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    GetComponent<CapsuleCollider2D>().enabled = false;  // 순간이동 시 충돌판정 제거
                    rb.MovePosition(rb.position + dir * teleportDistance);
                    //TODO: 순간이동 애니메이션 추가
                    teleportCoolTime = Time.time + 1f;
                }
            }

            GetComponent<CapsuleCollider2D>().enabled = true;
            animator.SetBool("isMoving", rb.velocity != Vector2.zero);
        }
    }

    private void Aim()
    {
        Vector2 mousePositionScreen = Input.mousePosition;
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        aim.up = mousePositionWorld - (Vector2)transform.position;

        Vector2 direction = mousePositionWorld - (Vector2)transform.position;
        if (direction.x > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else if (direction.x < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }

        Vector2 aimPosition = direction.normalized * 2.5f;

        aim.transform.position = new Vector2(transform.position.x + aimPosition.x, transform.position.y + aimPosition.y);
    }
}
