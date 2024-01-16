using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour
{
    private void Start()
    {
        DataManager.Instance.LoadGameData();
    }

    private void OnApplicationQuit()
    {
        DataManager.Instance.SaveGameData();
    }

    //public void StageUnlock(int stageNum)
    //{
    //    DataManager.Instance.data.isUnlock[stageNum] = true;

    //    DataManager.Instance.SaveGameData();
    //}
}
