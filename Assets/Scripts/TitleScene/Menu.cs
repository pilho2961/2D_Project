using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour, IPointerEnterHandler
{
    public Button newGameButton;
    public Button continueButton;
    public Button settingsButton;
    public Button quitButton;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        newGameButton.onClick.AddListener(() => DataManager.Instance.NewGameData());
        newGameButton.onClick.AddListener(() => SceneLoader.Instance.GameSceneLoad());
        continueButton.onClick.AddListener(() => SceneLoader.Instance.GameSceneLoad());
        settingsButton.onClick.AddListener(() => UIManager.Instance.PopUpSettings());
        quitButton.onClick.AddListener(() => GameManager.Instance.QuitGame());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.Play();
    }
}
