using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    //[SerializeField]
    //private GameObject pauseButton;

    // public Animator settingsButton;
    // public Animator quitButton;
    // public Animator menuButton;
    public Animator buttons;

    //public Animator dialog;
    //public Animator finalCutscene; TODO end cutscene
    public Animator animator;

    public AudioSource backgroundMusic;

    public bool paused = false;

    // void Awake()
    // {
    //     animator = GameObject.Find("Transition").GetComponent<Animator>();
    // }

    void Start()
    {
        Time.timeScale = 1;
    }

    public void Quit()
    {
        //If we are running in a standalone build of the game
#if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
#endif
        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor
            .EditorApplication
            .isPlaying = false;
#endif
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        //pauseButton.SetActive(false);
        // settingsButton.SetBool("isHidden", false);
        // quitButton.SetBool("isHidden", false);
        // menuButton.SetBool("isHidden", false);
        buttons.SetBool("isPaused", true);
        backgroundMusic.Pause();
        paused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        // settingsButton.SetBool("isHidden", true);
        // quitButton.SetBool("isHidden", true);
        // menuButton.SetBool("isHidden", false);
        buttons.SetBool("isPaused", false);
        pauseMenu.SetActive(false);
        //pauseButton.SetActive(true);
        backgroundMusic.Play();
        paused = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("Escape Key Pressed.");
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
    // public void OpenDialog()
    // {
    //     //dialog.SetBool("isHidden", false);
    //     // settingsButton.SetBool("isHidden", true);
    //     quitButton.SetBool("isHidden", true);
    //     menuButton.SetBool("isHidden", true);
    // }

    // public void CloseDialogue()
    // {
    //     //dialog.SetBool("isHidden", true);
    //     // settingsButton.SetBool("isHidden", false);
    //     quitButton.SetBool("isHidden", false);
    //     menuButton.SetBool("isHidden", false);
    // }

    // public void StartFinalCutscene()
    // {
    //     StopAllCoroutines();
    //     finalCutscene.SetBool("finalCutscene", true);
    // }
}
