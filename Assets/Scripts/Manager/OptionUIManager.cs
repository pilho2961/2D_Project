using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUIManager : MonoBehaviour
{
    private bool isPaused = false;
    private bool soundWindowOpened = false;
    private bool quitWindowOpened = false;
    public GameObject OptionUI;
    public GameObject SoundControlUI;
    public GameObject QuitGameWindow;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                if (!soundWindowOpened && !quitWindowOpened)
                {
                    ContinueGame();
                }
                else if (soundWindowOpened)
                {
                    SoundOptionOff();
                }
                else if (quitWindowOpened)
                {
                    CloseQuitGameWindow();
                }
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        OptionUI.SetActive(true);
        isPaused = true;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        OptionUI.SetActive(false);
        isPaused = false;
    }

    public void SoundOption()
    {
        soundWindowOpened = true;
        SoundControlUI.SetActive(true);
    }

    public void SoundOptionOff()
    {
        soundWindowOpened = false;
        SoundControlUI.SetActive(false);
    }

    public void OpenQuitGameWindow()
    {
        quitWindowOpened = true;
        QuitGameWindow.SetActive(true);
    }

    public void CloseQuitGameWindow()
    {
        quitWindowOpened = false;
        QuitGameWindow.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
