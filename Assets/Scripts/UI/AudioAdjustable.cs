using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAdjustable : MonoBehaviour
{
    public AudioType audioType;

    private AudioManager audioManager;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioManager (there should only be one).
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogWarning("No AudioManager established in scene!");
            return;
        }

        // Subscribe to the appropriate event.
        switch (audioType)
        {
            case AudioType.BGM:
                audioManager.BGMChange += UpdateAudio;
                break;
            case AudioType.SFX:
                audioManager.SFXChange += UpdateAudio;
                break;
            case AudioType.Menu:
                audioManager.MenuChange += UpdateAudio;
                break;
            default:
                Debug.LogWarning("Invalid Type");
                break;
        }

        // Initially reset the volume of this sound source (to the set default.)
        audioSource = this.gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource on AudioAdjustable GameObject");
            return;
        }
        audioSource.volume = audioManager.defaultVolume;
    }

    void UpdateAudio(float newAudio)
    {
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogWarning("GameObject with AudioAdjustable does not have an AudioSource!");
            return;
        }

        audioSource.volume = newAudio;
    }
}
