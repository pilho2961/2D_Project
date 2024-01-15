using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEffectParticle : MonoBehaviour
{
    public ParticleSystem runLeft;
    public ParticleSystem runRight;
    public GameObject player;

    private void Update()
    {
        player = GameManager.Instance.currentplayer;

        if (player.TryGetComponent(out Rigidbody2D rb))
        {
            if (rb.velocity.x >= 3f)
            {
                RunRight();
            }
            else if (rb.velocity.x <= -3f)
            {
                RunLeft();
            }
        }
    }

    public void RunLeft()
    {
        runLeft.Play();
        runRight.Stop();
    }

    public void RunRight()
    {
        runRight.Play();
        runLeft.Stop();
    }
}
