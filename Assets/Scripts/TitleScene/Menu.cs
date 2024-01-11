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
        //TODO: �޴�â�� ���� Ŀ���鼭 ��Ÿ������ (Ÿ��Ʋ �۾��� �۾����� �Ͱ� �ݴ��)
    }
}
