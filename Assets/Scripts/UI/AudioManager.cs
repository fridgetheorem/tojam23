using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Static values that stay consistent on scene changes.
    public static float volumeBGM = 1f;
    public static float volumeSFX = 1f;
    public static float volumeMenu = 1f;

    // Volume sliders
    [SerializeField]
    private Slider sliderBGM;

    [SerializeField]
    private Slider sliderSFX;

    [SerializeField]
    private Slider sliderMenu;

    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        SetBGMChanges();
        SetSFXChanges();
        SetMenuChanges();
    }

    public void TogglePanel()
    {
        animator.SetBool("Opened", !animator.GetBool("Opened"));
    }

    public void SetBGMChanges()
    {
        BGMChange?.Invoke(sliderBGM.value);
    }

    public void SetSFXChanges()
    {
        SFXChange?.Invoke(sliderSFX.value);
    }

    public void SetMenuChanges()
    {
        MenuChange?.Invoke(sliderMenu.value);
    }
}
