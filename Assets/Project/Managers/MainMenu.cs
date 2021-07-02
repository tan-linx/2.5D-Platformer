using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuUI;
    
    [SerializeField]
    private GameObject pauseMenuUI;
    
    void OnGUI()
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
        Debug.Log("Quit");
        Application.Quit();
    }
}
