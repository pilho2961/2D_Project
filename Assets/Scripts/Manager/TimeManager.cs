using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    // 스킬별 마지막 사용 시간을 저장할 딕셔너리
    private readonly Dictionary<string, float> lastSkillUsageTime = new Dictionary<string, float>();

    // 스킬 사용 가능 여부를 체크하는 함수
    public bool IsSkillAvailable(string skillName, float cooldown)
    {
        if (lastSkillUsageTime.TryGetValue(skillName, out float lastUsageTime))
        {
            float elapsedTime = Time.time - lastUsageTime;
            return elapsedTime >= cooldown;
        }
        else
        {
            // 스킬이 등록되어 있지 않으면 등록하고 사용 가능
            lastSkillUsageTime[skillName] = Time.time;
            return true;
        }
    }

    // 스킬 사용 후 호출하는 함수
    public void UseSkill(string skillName)
    {
        // 스킬 사용 시간 갱신
        lastSkillUsageTime[skillName] = Time.time;
    }
}
