using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MenuManager
{
    public Animator transition;

    [SerializeField]
    private float _transitionTime = 1f;

    public Animator endScreen;

    private DialogueManager dialogueManager;

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetBool("Start", true);
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator Quitting()
    {
        transition.SetBool("Start", true);
        yield return new WaitForSeconds(_transitionTime);
        base.QuitGame();
    }

    void Start()
    {
        Time.timeScale = 1f;

        // Subscribe the last dialogue
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager)
        {
            dialogueManager.LastDialogue += EndCutscene;
            dialogueManager.GameOver += OnGameOver;
        }
    }

    public void StartGame()
    {
        StartCoroutine(LoadLevel(1));
    }

    public void OnGameOver()
    {
        Debug.Log("game ending caught");
        StartCoroutine(LoadLevel(0));
    }

    public void FadeToBlack()
    {
        transition.SetBool("Start", true);
    }

    public void FadeBackIn()
    {
        transition.SetBool("Start", false);
    }

    public void EndCutscene()
    {
        if (endScreen)
            endScreen.SetBool("Start", true);

        Debug.Log("End Cutscene triggered");
    }

    public override void MainMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(0));
    }

    public void Restart()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadLevel(1));
    }

    public override void QuitGame()
    {
        Time.timeScale = 1;
        StartCoroutine(Quitting());
    }

    void Destroy()
    {
        if (dialogueManager)
        {
            dialogueManager.LastDialogue -= EndCutscene;
            dialogueManager.GameOver -= OnGameOver;
        }
    }
}
