using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenu;

    PlayerControls p;    
    void Awake()
    {
        p = new PlayerControls();
        p.General.esc.performed += c => TogglePause();
    }

    void OnEnable()
    {
        p.General.Enable();
    }

    void OnDisable()
    {
        p.General.Disable();
    }
    void TogglePause()
    {
        if(GameIsPaused)
            Resume();
        else Pause();
    }

    void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
