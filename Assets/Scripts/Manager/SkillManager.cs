using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }
    public GameObject[] skillPrefabs;
    public Transform aim;
    public List<GameObject> moonSlice;
    public List<GameObject> usedMoonSlice;


    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    void Start()
    {
        for (int i = 0; i < 50;  i++)
        {
            GameObject moon = Instantiate(skillPrefabs[0], transform);
            moon.SetActive(false);
            moonSlice.Add(moon);
        }
    }

    void Update()
    {
        
    }

    public bool IsSkillReady(float skillCoolTime)
    {
        return Time.time >= skillCoolTime;
    }

    public void MoonSlice()
    {
        moonSlice[moonSlice.Count - 1].SetActive(true);
        moonSlice[moonSlice.Count - 1].transform.position = GameManager.Instance.currentplayer.transform.position;
        moonSlice[moonSlice.Count - 1].transform.rotation = aim.rotation;
        moonSlice[moonSlice.Count - 1].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        usedMoonSlice.Add(moonSlice[moonSlice.Count - 1]);
        moonSlice.RemoveAt(moonSlice.Count - 1);

        if (moonSlice.Count < 10)
        {
            ResetMoonSlice();
        }
    }

    public void StrongMoonSlice()
    {
        moonSlice[moonSlice.Count - 1].SetActive(true);
        moonSlice[moonSlice.Count - 1].transform.position = GameManager.Instance.currentplayer.transform.position;
        moonSlice[moonSlice.Count - 1].transform.rotation = aim.rotation;
        moonSlice[moonSlice.Count - 1].transform.localScale = new Vector3(1f, 1f, 1f);

        usedMoonSlice.Add(moonSlice[moonSlice.Count - 1]);
        moonSlice.RemoveAt(moonSlice.Count - 1);

        if (moonSlice.Count < 10)
        {
            ResetMoonSlice();
        }
    }

    public void ResetMoonSlice()
    {
        for (int i = 0; i < 40; i++)
        {
            moonSlice.Add(usedMoonSlice[usedMoonSlice.Count - 1]);
            usedMoonSlice.RemoveAt(usedMoonSlice.Count - 1);
        }
    }
}
