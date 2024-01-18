using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    Button closeButton;
    public AudioMixer mixer;

    // º¼·ý Á¶Àý
    public void SetLevel(float sliderVal)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderVal) * 20);
    }

    private void Awake()
    {
        closeButton = GetComponentInChildren<Button>();
    }

    void Start()
    {
        closeButton.onClick.AddListener(() => UIManager.Instance.CloseSettingsPanel());
    }
}
