using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MenuManager
{
    [SerializeField]
    private GameObject pauseMenu;

    public AudioSource backgroundMusic;

    public bool paused = false;

    public delegate void OnPause();
    public event OnPause GamePaused;
    public delegate void OnResume();
    public event OnResume GameResumed;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        backgroundMusic.Pause();
        paused = true;
        GamePaused?.Invoke();
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;

        pauseMenu.SetActive(false);
        backgroundMusic.Play();
        paused = false;
        GameResumed?.Invoke();
    }

    public void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }
}
