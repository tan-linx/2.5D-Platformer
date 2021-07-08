using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// based on https://www.youtube.com/watch?v=zc8ac_qUXQY&t=660s
// modified: Exit() so that game restarts on the same scene
public class PauseMenu : MonoBehaviour
{
    private static bool paused;
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject mainMenuUI;

    void Awake()
    {
        pauseMenuUI.SetActive(false);
        paused = false;
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
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
        paused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void ExitLoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
