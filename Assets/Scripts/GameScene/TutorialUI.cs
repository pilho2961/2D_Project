using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public GameObject tutorialUI;

    private void Start()
    {
        OpenOnlyStart();
    }

    public void OpenOnlyStart()
    {
        for (int i = 0; i < DataManager.Instance.data.isUnlock.Length; i++)
        {
            if (DataManager.Instance.data.isUnlock[i])
            {
                tutorialUI.SetActive(false);
            }
        }
    }

    public void EndTutorial()
    {
        tutorialUI.SetActive(false);
    }
}
