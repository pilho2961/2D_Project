using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyDreamFragmentE : MonoBehaviour
{
    public float fearGage;
    private Vector2 velocity = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(MoveTowardPlayer(other));
            Invoke("Eat", 0.7f);
        }
    }

    private void Eat()
    {
        FragmentPoolManager.Instance.HideAteFragments(this);
    }

    private void OnEnable()
    {
        StartCoroutine(FloatingAnimation());
    }

    private IEnumerator FloatingAnimation()
    {
        while(true)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + Mathf.Sin(Time.time) * 0.0002f);
            yield return null;
        }
    }

    private IEnumerator MoveTowardPlayer(Collider2D other)
    {
        while(true)
        {
            transform.position = Vector2.SmoothDamp(gameObject.transform.position, other.transform.position, ref velocity, 0.15f);
            yield return null;
        }
    }
}
