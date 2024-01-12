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

    // ��ų�� ������ ��� �ð��� ������ ��ųʸ�
    private readonly Dictionary<string, float> lastSkillUsageTime = new Dictionary<string, float>();

    // ��ų ��� ���� ���θ� üũ�ϴ� �Լ�
    public bool IsSkillAvailable(string skillName, float cooldown)
    {
        if (lastSkillUsageTime.TryGetValue(skillName, out float lastUsageTime))
        {
            float elapsedTime = Time.time - lastUsageTime;
            return elapsedTime >= cooldown;
        }
        else
        {
            // ��ų�� ��ϵǾ� ���� ������ ����ϰ� ��� ����
            lastSkillUsageTime[skillName] = Time.time;
            return true;
        }
    }

    // ��ų ��� �� ȣ���ϴ� �Լ�
    public void UseSkill(string skillName)
    {
        // ��ų ��� �ð� ����
        lastSkillUsageTime[skillName] = Time.time;
    }
}
