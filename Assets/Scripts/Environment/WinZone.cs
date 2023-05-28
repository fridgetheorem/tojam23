using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    LevelLoader levelLoader;

    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        if (levelLoader == null)
        {
            Debug.LogWarning("No level loader to end game level");
        }
    }

    public void winGame()
    {
        levelLoader.OnGameOver();
        Debug.Log("Win");
    }

    //Upon collision with another an animal object, win is triggered
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<AnimalController>() != null)
        {
            winGame();
        }
    }
}
