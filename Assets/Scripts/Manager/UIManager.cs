using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public GameObject pressAnyKeyText;
    public GameObject menuPrefab;
    private GameObject menuInstance;

    public GameObject settingsPrefab;
    private GameObject settingsInstance;
    public ParticleSystem butterflyParticle;

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
        TitleSceneMotion();
    }

    public void TitleSceneMotion()
    {
        if (Input.anyKeyDown && butterflyParticle != null)
        {
            pressAnyKeyText.SetActive(false);
            butterflyParticle.Play();
            butterflyParticle = null;
            menuInstance = Instantiate(menuPrefab);
        }
    }

    public void PopUpSettings()
    {
        settingsInstance = Instantiate(settingsPrefab);
        Destroy(menuInstance);
    }

    public void CloseSettingsPanel()
    {
        Destroy(settingsInstance);
        menuInstance = Instantiate(menuPrefab);
    }
}
