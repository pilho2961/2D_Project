using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSound : MonoBehaviour
{
    AudioSource m_Source;
    bool hasPlayed = false;

    private void Awake()
    {
        m_Source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            m_Source.Play();
            hasPlayed = true;
        }
    }
}
