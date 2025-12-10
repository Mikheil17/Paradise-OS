using UnityEngine;
using System.Collections;

public class UIFadeAway : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 2f;

    public void Start()
    {
        if (canvasGroup != null)
            StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;
        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}
