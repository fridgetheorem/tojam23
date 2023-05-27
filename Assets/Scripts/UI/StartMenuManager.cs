using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    // TODO: Implement settings
    //public Animator startButton;
    //public Animator settingsButton;
    //public Animator dialog;
    public Animator animator;

    // void Awake()
    // {
    //     animator = GameObject.Find("Transition").GetComponent<Animator>();
    // }

    void Start()
    {
        Time.timeScale = 1;
    }

    // public void OpenMenuSettings()
    // {
    //     startButton.SetBool("isHidden", true);
    //     settingsButton.SetBool("isHidden", true);
    //     dialog.SetBool("isHidden", false);
    // }

    public void StartGame()
    {
        animator.SetTrigger("TriggerTransition");
        SceneManager.LoadScene(1); // Main game (scene will change)
    }

    public void EndGame()
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

    // public void CloseMenuSettings()
    // {
    //     startButton.SetBool("isHidden", false);
    //     settingsButton.SetBool("isHidden", false);
    //     dialog.SetBool("isHidden", true);
    // }
}
