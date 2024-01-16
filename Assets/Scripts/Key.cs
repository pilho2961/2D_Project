using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int keyNum;
    private Vector2 velocity = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(MoveTowardPlayer(other));
            Invoke("Eat", 0.4f);
        }
    }

    private void Eat()
    {
        DataManager.Instance.StageUnlock(keyNum);
        gameObject.SetActive(false);
        SceneLoader.Instance.GameSceneLoad();
    }

    private void OnEnable()
    {
        StartCoroutine(Floating());
    }

    private IEnumerator Floating()
    {
        while (true)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + Mathf.Sin(Time.time) * 0.0005f);
            yield return null;
        }
    }

    private IEnumerator MoveTowardPlayer(Collider2D other)
    {
        while (true)
        {
            transform.position = Vector2.SmoothDamp(gameObject.transform.position, other.transform.position, ref velocity, 0.15f);
            yield return null;
        }
    }
}
