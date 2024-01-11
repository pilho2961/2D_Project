using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    public void GameSceneLoad()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void TitleSceneLoad()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
