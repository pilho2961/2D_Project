using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    private Vector2 initialScale;
    private Vector2 targetScale;
    private float shrinkTime;
    private float elapsedTime;

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    private void Start()
    {
        targetScale = new Vector2(0.6f, 0.6f);
        shrinkTime = 1.0f;
    }

    void Update()
    {
        if (!UIManager.Instance.pressAnyKeyText.activeSelf)
        {
            Shrink();
        }
    }

    private void Shrink()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / shrinkTime);

        Vector2 changeScale = new Vector2(Mathf.Lerp(initialScale.x, targetScale.x, t),
            Mathf.Lerp(initialScale.y, targetScale.y, t));

        transform.localScale = changeScale;
    }
}
