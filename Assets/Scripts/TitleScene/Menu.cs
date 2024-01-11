using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    Animator animator;
    public Button newGameButton;
    public Button settingsButton;
    public Button quitButton;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    private void Start()
    {
        newGameButton.onClick.AddListener(() => SceneLoader.Instance.GameSceneLoad());
        settingsButton.onClick.AddListener(() => UIManager.Instance.PopUpSettings());
        quitButton.onClick.AddListener(() => GameManager.Instance.QuitGame());
    }

    private void Update()
    {
        //TODO: 메뉴창이 점점 커지면서 나타나도록 (타이틀 글씨가 작아지는 것과 반대로)
    }
}
