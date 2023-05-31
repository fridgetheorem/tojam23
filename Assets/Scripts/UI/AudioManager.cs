using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType // your custom enumeration
{
    BGM,
    SFX,
    Menu
};

public class AudioManager : MonoBehaviour
{
    // Delegates that will be invoked on audio change.
    public delegate void OnBGMChange(float newAudio);
    public event OnBGMChange BGMChange;

    public delegate void OnSFXhange(float newAudio);
    public event OnSFXhange SFXChange;

    public delegate void OnMenuChange(float newAudio);
    public event OnMenuChange MenuChange;

    public delegate void OnAudioChange(float newAudio);
    public event OnAudioChange AudioChange;

    // Static values that stay consistent on scene changes.
    public static float volumeBGM = 1f;
    public static float volumeSFX = 1f;
    public static float volumeMenu = 1f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
