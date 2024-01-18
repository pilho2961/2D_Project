using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject currentplayer;
    public GameObject sleepCharacter;
    public GameObject metamorphosisPrefab;
    public GameObject playerYoungPrefab;
    public GameObject collectionNotice;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(Instance);
        }
        Instance = this;
    }

    public void Metamorphosis()
    {
        Transform currentplayerPos = currentplayer.transform;
        sleepCharacter = currentplayer;
        sleepCharacter.SetActive(false);
        currentplayer = Instantiate(metamorphosisPrefab, currentplayerPos.position, Quaternion.identity);
    }

    public void TurnToNormal()
    {
        sleepCharacter.SetActive(true);
        sleepCharacter.transform.position = currentplayer.transform.position;
        currentplayer.SetActive(false);
        currentplayer = sleepCharacter;
        sleepCharacter = null;
    }

    public void QuitGame()
    {
        print("게임 종료");
        Application.Quit();
    }
    public void BreakingIce()
    {
        collectionNotice.SetActive(true);
        Transform currentplayerPos = currentplayer.transform;
        sleepCharacter = currentplayer;
        sleepCharacter.SetActive(false);
        currentplayer = Instantiate(playerYoungPrefab, currentplayerPos.position, Quaternion.identity);
    }
}
