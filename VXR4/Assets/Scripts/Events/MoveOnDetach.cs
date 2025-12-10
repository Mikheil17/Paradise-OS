using UnityEngine;
using System.Collections;

public class MoveOnDetach : MonoBehaviour
{
    private Transform parent;
    private Vector3 initialOffset;
    public float detachDistance = 0.1f;
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.5f;

    private bool hadDetached = false;
    private SaveManager saveManager;

    void Start()
    {
        saveManager = SaveManager.instance;

        parent = transform.parent;
        if (parent != null)
            initialOffset = transform.position - parent.position;
    }

    void Update()
    {
        if (!hadDetached && parent != null)
        {
            float distance = (transform.position - (parent.position + initialOffset)).magnitude;
            if (distance > detachDistance)
            {
                hadDetached = true;

                if (canvasGroup != null)
                    StartCoroutine(FadeToAlpha(1f, fadeDuration));
            }
        }
    }

    private IEnumerator FadeToAlpha(float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;
        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;

        saveManager.GotoNextScene();
    }
}
