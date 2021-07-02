using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.youtube.com/watch?v=JivuXdrIHK0
public class PauseMenu : MonoBehaviour
{
    private static bool Paused;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject mainMenuUI;

    void Awake()
    {
        pauseMenuUI.SetActive(false);
        Paused = false;
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }

    public void ExitLoadMenu()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
