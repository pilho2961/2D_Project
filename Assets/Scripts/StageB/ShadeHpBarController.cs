using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadeHpBarController : MonoBehaviour
{
    public Slider healthSlider;
    public Transform monsterTransform;
    public Vector3 offset = new Vector3(0f, 1f, 0f);

    void Update()
    {
        if (monsterTransform != null)
        {
            Vector3 screenPos = monsterTransform.position + offset;
            healthSlider.transform.position = screenPos;
        }
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        if (currentHealth > 0f)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
        else if (currentHealth <= 0f)
        {
            healthSlider.value = currentHealth / maxHealth;
            gameObject.SetActive(false);

        }
    }


}
