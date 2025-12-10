using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEndManager : MonoBehaviour
{

    public CanvasGroup whiteoutCanvas; 
    public float duration = 2f;

    private bool hasTriggered = false;

    private SaveManager saveManager;

    void Start()
    {
        saveManager = SaveManager.instance;
    }

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
        if (hasTriggered) return;
        hasTriggered = true;

        if (whiteoutCanvas != null)
        {
            StartCoroutine(FadeToWhite());
        }
    }

    private IEnumerator FadeToWhite()
    {
        float startAlpha = whiteoutCanvas.alpha;
        float time = 0f;
        while (time < duration)
        {
            whiteoutCanvas.alpha = Mathf.Lerp(startAlpha, 1f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        whiteoutCanvas.alpha = 1f;

        saveManager.GotoNextScene();
    }
}