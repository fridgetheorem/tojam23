using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public virtual void QuitGame()
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

    public virtual void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public virtual void LoadCredits()
    {
        GameObject
            .FindGameObjectWithTag("EndCredits")
            .GetComponent<Animator>()
            .SetTrigger("ShowCredits");

        GameObject
            .FindGameObjectWithTag("EndCredits")
            .GetComponent<Animator>()
            .ResetTrigger("HideCredits");
    }

    public virtual void HideCredits()
    {
        GameObject
            .FindGameObjectWithTag("EndCredits")
            .GetComponent<Animator>()
            .SetTrigger("HideCredits");

        GameObject
            .FindGameObjectWithTag("EndCredits")
            .GetComponent<Animator>()
            .ResetTrigger("ShowCredits");
    }
}
