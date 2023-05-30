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
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        backgroundMusic.Play();
        paused = false;
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
