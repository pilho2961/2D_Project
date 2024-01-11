using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }
    public GameObject[] skillPrefabs;


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
        GameObject moon = Instantiate(skillPrefabs[0]);
    }
}
