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
}
