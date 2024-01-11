using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEffectParticle : MonoBehaviour
{
    public ParticleSystem runLeft;
    public ParticleSystem runRight;

    public void RunLeft()
    {
        runLeft.Play();
    }

    public void RunRight()
    {
        runRight.Play();
    }

    public void NotRunning()
    {
        //if 
    }
}
