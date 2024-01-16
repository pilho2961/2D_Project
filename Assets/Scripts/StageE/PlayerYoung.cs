using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerYoung : MonoBehaviour
{
    public bool canMove = true;
    public float speed;
    public float stamina;
    Animator animator;
    Rigidbody2D rb;

    private float maxFear;
    [SerializeField]
    private float currentFear;
    Slider fearBar;
    TextMeshProUGUI fearText;
    public float GetFearValue() { return currentFear / maxFear; }

    HappyDreamFragmentE fragment;
    FogOfWar fog;
    public int happyCount;
    public int badCount;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        fearBar = GetComponentInChildren<Slider>();
        fearText = GetComponentInChildren<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        FearBarChange();
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

            if (canMove && stamina > 0 && Input.GetKey(KeyCode.LeftShift))
            {
                speed = 4f;
            }
            else
            {
                speed = 2f;
            }

            dir.Normalize();
            rb.velocity = speed * dir;
            animator.SetBool("isMoving", rb.velocity != Vector2.zero);
            if (dir.x < 0)
            {
                gameObject.transform.localScale = new Vector2(1, gameObject.transform.localScale.y);
            }
            else if (dir.x > 0)
            {
                gameObject.transform.localScale = new Vector2(-1, gameObject.transform.localScale.y);
            }
            animator.SetBool("isStop", rb.velocity ==  Vector2.zero);
        }
        else if (!canMove)
        {
            rb.velocity = Vector2.zero;
        }
    }
    
    public void FearBarChange()
    {
        if (currentFear < 0) { currentFear = 0; }
        if (currentFear > maxFear) { currentFear = maxFear; }
        fearBar.value = GetFearValue();
        fearText.text = currentFear.ToString();
    }

    // 프리팹이 생성된 이후, 3초마다 2씩 공포게이지가 차오르는 함수
    private void OnEnable()
    {
        maxFear = 50f;
        currentFear = 20f;
        stamina = 4f;
        StartCoroutine(TimeLimit());
        StartCoroutine(HealStamina());
        StartCoroutine(UseStamina());
    }

    private IEnumerator TimeLimit()
    {
        while(true)
        {
            if (currentFear < maxFear)
            {
                yield return new WaitForSeconds(3);
                animator.SetTrigger("isFear");
                currentFear += 2;
            }
            else if (currentFear >= maxFear)
            {
                Sturned();
                yield return new WaitForSeconds(5);
                canMove = true;
                currentFear -= 10;
            }
        }
    }

    private IEnumerator HealStamina()
    {
        while (true)
        {
            if (speed < 4f && stamina < 4)
            {
                stamina++;
            }
            yield return new WaitForSeconds(2);
        }
    }

    private IEnumerator UseStamina()
    {
        while (true)
        {
            if (speed > 2f && stamina > 0)
            {
                stamina--;
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void Sturned()
    {
        canMove = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Happy" || other.tag == "Bad")
        {
            fragment = other.GetComponent<HappyDreamFragmentE>();
            currentFear += fragment.fearGage;
            if (currentFear < 0) { currentFear = 0; }
            if (currentFear > maxFear) { currentFear = maxFear; }
            if (other.CompareTag("Happy"))
            {
                happyCount++;
            }
            else
            {
                badCount++;
            }
        }
    }
}
