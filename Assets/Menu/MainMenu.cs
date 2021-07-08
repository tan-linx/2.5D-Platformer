using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// based on https://www.youtube.com/watch?v=zc8ac_qUXQY&t=660s
// modified: 
// -Update() and Play() so that menu is visible on the main scene 
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuUI;
    
    [SerializeField]
    private GameObject pauseMenuUI;
    
    void Update()
    {
        if (mainMenuUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else 
        {
            if (!pauseMenuUI.activeSelf)
                Time.timeScale = 1f;
        }
    }

    public void Play()
    {
        Time.timeScale = 1f;
        mainMenuUI.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
