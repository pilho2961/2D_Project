using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseAnger : MonoBehaviour
{
    public GameObject anger;

    private void OnEnable()
    {
        int randNum = Random.Range(3, 6);
        for (int i = 0; i < randNum; i++)
        {

            GameObject generatedAnger = Instantiate(anger);

        }
    }
}
