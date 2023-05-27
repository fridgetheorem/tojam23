using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private GameControls _gameControls;
    // Start is called before the first frame update
    void Awake()
    {
        _gameControls = new GameControls();
    }

    private void OnEnable(){
        _gameControls.Enable();
    }

    private void OnDisable(){
        _gameControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = _gameControls.Player.Movement.ReadValue<Vector2>();
        Debug.Log(move);
    }
}
