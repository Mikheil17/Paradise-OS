using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class incorrectHeadRemoval : MonoBehaviour
{
    public AudioSource headsetRemovalAudio;

    void OnEnable()
    {
        OVRManager.HMDUnmounted += OnHMDUnmounted;
    }

    void OnDisable()
    {
        OVRManager.HMDUnmounted -= OnHMDUnmounted;
    }

    private void OnHMDUnmounted()
    {
        if (headsetRemovalAudio != null)
        {
            headsetRemovalAudio.Play();
        }
    }
}