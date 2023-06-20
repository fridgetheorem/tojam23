using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    private PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;

    [SerializeField]
    private float offset = 2f;

    void Start()
    {
        postProcessVolume = Object.FindObjectOfType<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings<DepthOfField>(out depthOfField);
    }

    void FixedUpdate()
    {
        Vector3 cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        Vector3 targetPosition = transform.parent.gameObject.transform.position;

        depthOfField.focusDistance.value = (cameraPosition - targetPosition).magnitude + offset;
    }
}
