using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool canMove = true;
    public float speed;
    public Transform aim;
    Animator animator;
    public float teleportDistance;
    private float teleportCoolTime = 0f;
    Rigidbody2D rb;

    public float metamorphosisGage;     // 변신 게이지

    private float maxHp;
    [SerializeField]
    public float currentHp;
    Slider hpBar;
    TextMeshProUGUI hpText;
    public float GetHpValue() { return currentHp / maxHp; }

    public ShadeBullet shadeBullet;
    public Anger anger;

    public Tilemap tilemap;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hpBar = GetComponentInChildren<Slider>();
        hpText = GetComponentInChildren<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody2D>();
        tilemap = FindObjectOfType<Tilemap>();
    }

    private void Start()
    {
        maxHp = 100f;
        currentHp = 100f;
        previousPosition = transform.position;
    }

    private void Update()
    {
        previousPosition = transform.position;
        Move();
        Aim();
        BasicMoonSliceAttack();
        StrongMoonSliceAttack();
        HpBarChange();
        UsingMetamorphosis();
        OntheTilemap();
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

            if (SkillManager.Instance.IsSkillReady(teleportCoolTime) && Input.GetKey(KeyCode.LeftShift))
            {
                GetComponent<CapsuleCollider2D>().enabled = false;  // 순간이동 시 충돌판정 제거
                rb.MovePosition(rb.position + dir * teleportDistance);
                //TODO: 순간이동 애니메이션 추가
                teleportCoolTime = Time.time + 1f;
            }

            GetComponent<CapsuleCollider2D>().enabled = true;
            animator.SetBool("isMoving", rb.velocity != Vector2.zero);
        }
    }

    // 플레이어가 타일맵 위에 위치하는지 체크하고 아니면 맵 위로 돌려보내는 함수
    private void OntheTilemap()
    {
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        if (!IsCellInsideTilemapBounds(cellPosition))
        {
            // If the player is outside the tilemap bounds, set the player's position to the end of the tilemap
            transform.position = tilemap.GetCellCenterWorld(cellPosition);
        }
    }
    bool IsCellInsideTilemapBounds(Vector3Int cellPosition)
    {
        return tilemap.cellBounds.Contains(cellPosition);
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

    private void OnEnable()
    {
        metamorphosisGage = 0;
        StartCoroutine(GainmetamorphosisGage());
    }

    private IEnumerator GainmetamorphosisGage()
    {
        while (metamorphosisGage < 100)
        {
            metamorphosisGage += 50;
            yield return new WaitForSeconds(1);
        }
    }

    private bool isAnimationPlaying = false;

    private void UsingMetamorphosis()
    {
        if (!isAnimationPlaying && metamorphosisGage >= 100 && Input.GetKey(KeyCode.F))
        {
            isAnimationPlaying = true;
            metamorphosisGage = 0;
            animator.SetTrigger("isChange");
        }
    }

    private void OnAnimationEnd()
    {
        ChangeOver();
        AnimationComplete();
        GameManager.Instance.Metamorphosis();
    }

    public void HpBarChange()
    {
        hpBar.value = GetHpValue();
        hpText.text = currentHp.ToString();
    }

    public void ChangeOver()
    {
        animator.SetTrigger("changeOver");
    }

    public void AnimationComplete()
    {
        isAnimationPlaying = false;
    }

    public void TakeDamage(float damage)
    {
        if (currentHp > damage)
        {
            currentHp -= damage;
        }
        if (currentHp <= damage)
        {
            currentHp = 0;
            Die();
        }
    }

    private void Die()
    {
        // TODO:캐릭터 비활성화하고 재도전 UI 띄우기 -> 씬 다시 불러오기
        return;
    }

    private void BasicMoonSliceAttack()
    {
        if (Input.GetMouseButtonDown(0) && TimeManager.Instance.IsSkillAvailable("BasicMoonSliceAttack", 0.5f))
        {
            SkillManager.Instance.MoonSlice();
            TimeManager.Instance.UseSkill("BasicMoonSliceAttack");
        }
        else if (Input.GetMouseButtonDown(0) && !TimeManager.Instance.IsSkillAvailable("BasicMoonSliceAttack", 0.5f))
        {
            print("moonslice쿨타임");
        }
    }

    private bool isRightMouseDown;
    private float rightMouseClickStartTime;

    private void StrongMoonSliceAttack()
    {
        if (Input.GetMouseButtonDown(1) && TimeManager.Instance.IsSkillAvailable("StrongMoonSlice", 3f)) // 1 represents the right mouse button
        {
            // Right mouse button pressed
            isRightMouseDown = true;
            rightMouseClickStartTime = Time.time;
            animator.SetBool("preparing", true);
        }
        else if (Input.GetMouseButtonDown(1) && !TimeManager.Instance.IsSkillAvailable("StrongMoonSlice", 3f))
        {
            print("strongmoonslice쿨타임");
        }

        if (Input.GetMouseButtonUp(1)) // 1 represents the right mouse button
        {
            animator.SetBool("preparing", false);
            // Right mouse button released
            if (isRightMouseDown)
            {
                float duration = Time.time - rightMouseClickStartTime;
                if (duration > 1.5f)
                {
                    SkillManager.Instance.StrongMoonSlice();
                    TimeManager.Instance.UseSkill("StrongMoonSlice");
                }
            }

            isRightMouseDown = false;
        }
    }

    private Vector3 previousPosition;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grass")
        {
            transform.position = previousPosition;
        }
    }
}
