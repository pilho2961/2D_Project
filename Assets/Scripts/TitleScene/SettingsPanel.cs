using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    Button closeButton;
    public float maxBGMSound;
    public float maxEffectSound;
    public float currentBGM;
    public float currentEffectSound;

    private void Awake()
    {
        closeButton = GetComponentInChildren<Button>();
    }

    void Start()
    {
        closeButton.onClick.AddListener(() => UIManager.Instance.CloseSettingsPanel());
    }
}
