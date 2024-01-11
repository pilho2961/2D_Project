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
            Vector2 randPos = new Vector2 (Random.Range(-2, 2), Random.Range(-2, 2));
            Vector2 randRot = new Vector2 (0, Random.Range(0f, 360f));
            GameObject generatedAnger = Instantiate(anger, randPos, Quaternion.identity, transform);
            generatedAnger.transform.Rotate(randRot);
        }
    }
}
