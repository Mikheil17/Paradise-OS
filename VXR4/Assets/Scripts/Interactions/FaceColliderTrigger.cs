using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FaceColliderTrigger : MonoBehaviour
{
    public GameObject vrFace;
    public CanvasGroup whiteoutCanvas;
    private SaveManager saveManager;

    public void Awake()
    {
        saveManager = SaveManager.instance;

        if (whiteoutCanvas != null)
        {
            whiteoutCanvas.alpha = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            StartCoroutine(FadeToWhite(3f)); // 3 second fade
        }
    }

    private IEnumerator FadeToWhite(float duration)
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
