using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentPoolManager : MonoBehaviour
{
    public static FragmentPoolManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    public GameObject[] fragmentPrefabs;
    public List<GameObject> createdFragments;
    public List<GameObject> usedFragments;

    private void Start()
    {
        CreatRandomFragments();
        ArrangeFragments();
    }

    private void Update()
    {
        GenerateInGame();
    }

    private void CreatRandomFragments()
    {
        for (int i = 0; i < 30;  i++)
        {
            int randomNum = Random.Range(0, 2);
            GameObject create = Instantiate(fragmentPrefabs[randomNum], transform);
            createdFragments.Add(create);
        }
    }

    private void ArrangeFragments()
    {
        for (int i = 0; i < createdFragments.Count; i++)
        {
            Transform arrangePos = createdFragments[i].transform;
            // TODO : 콜라이더를 제외한 맵 안에 fragments가 배치되도록 변경
            arrangePos.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }
    }

    public void HideAteFragments(HappyDreamFragmentE fragment)
    {
        usedFragments.Add(fragment.gameObject);
        createdFragments.Remove(fragment.gameObject);
        fragment.gameObject.SetActive(false);
    }

    public void GenerateInGame()
    {
        if (createdFragments.Count < 10)
        {
            for (int i = 0; i < 5; i++)
            {
                int randomNum = Random.Range(0, 2);
                GameObject newFragments = Instantiate(fragmentPrefabs[randomNum], transform);
                createdFragments.Add(newFragments);
                // TODO : 콜라이더를 제외한 맵 안에 fragments가 배치되도록 변경
                newFragments.transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
            }
        }
    }
}
