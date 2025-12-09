using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class loadingScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup loadingCanvas;
    public Slider loadingBar;
    [Header("Audio")]
    public AudioSource loadingAudioSource;
    public AudioClip loadingSound;
    [Header("Loading Settings")]
    public float loadingDuration = 0f; // If 0, will use sound length

    private void Start()
    {
        StartCoroutine(ShowLoadingScreen());
    }

    private IEnumerator ShowLoadingScreen()
    {
        if (loadingCanvas != null)
            loadingCanvas.alpha = 1f;
        if (loadingBar != null)
            loadingBar.value = 0f;

        float duration = loadingDuration;
        if ((duration <= 0f) && loadingSound != null)
        {
            duration = loadingSound.length;
        }

        if (loadingAudioSource != null && loadingSound != null)
        {
            loadingAudioSource.clip = loadingSound;
            loadingAudioSource.loop = false;
            loadingAudioSource.Play();
        }

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            if (loadingBar != null)
                loadingBar.value = Mathf.Clamp01(timer / duration);
            yield return null;
        }

        if (loadingAudioSource != null)
            loadingAudioSource.Stop();
        if (loadingCanvas != null)
            loadingCanvas.alpha = 0f;
        // Loading complete, continue to next step here
    }
}
