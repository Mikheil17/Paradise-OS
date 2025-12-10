using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Explode : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public AudioSource explosionSound;
    public GameObject explosionEffect;   // particle system or light
    public GameObject uiCanvas;          // your UI canvas to disable
    public GameObject videoScreen;       // the screen or object holding the video
    public CanvasGroup secondaryCanvasGroup;

    private SaveManager saveManager;

    private void Start()
    {
        saveManager = SaveManager.instance;

        // Ensure explosion effect starts off
        if (explosionEffect != null)
            explosionEffect.SetActive(false);

        // Start the timer for the video length, then run Destroy
        StartCoroutine(WaitAndDestroy());
    }

    // Enumerator that waits for the length of the video
    private IEnumerator WaitAndDestroy()
    {
        if (videoPlayer != null && videoPlayer.clip != null)
        {
            yield return new WaitForSeconds((float)videoPlayer.clip.length - 1.0f);
        }

        Destroy();
    }

    private void Destroy()
    {

        // Disable the video screen
        if (videoScreen != null)
            videoScreen.SetActive(false);

        // Disable UI canvas
        if (uiCanvas != null)
            uiCanvas.SetActive(false);

        // Activate explosion VFX
        if (explosionEffect != null)
        {
            explosionEffect.SetActive(true);

            var ps = explosionEffect.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                ps.Play();
            }
        }

        if (secondaryCanvasGroup != null)
            secondaryCanvasGroup.alpha = 1f;

        // Play explosion audio
        if (explosionSound != null)
            explosionSound.Play();

        StartCoroutine(forceEndExperience());
    }

    IEnumerator forceEndExperience()
    {
        yield return new WaitForSeconds(1.5f);

        saveManager.GotoNextScene();
    }
}
