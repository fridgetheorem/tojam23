using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MenuManager
{
    public Animator transition;

    [SerializeField]
    private float _transitionTime = 1f;

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetBool("Start", true);
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    void Start()
    {
        Time.timeScale = 1f;

        // Subscribe the last dialogue
        DialogueManager dm = FindObjectOfType<DialogueManager>();
        if (dm)
        {
            dm.LastDialog += EndCutscene;
            dm.GameOver += OnGameOver;
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
        FadeToBlack();

        Debug.Log("End Cutscene triggered");
    }
}
