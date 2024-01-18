using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBrestart : MonoBehaviour
{
    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneLoader.Instance.StageBSceneLoad();
    }

    public void GobackGameScene()
    {
        Time.timeScale = 1.0f;
        SceneLoader.Instance.GameSceneLoad();
    }
}
