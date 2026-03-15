using UnityEngine;
using System.Collections;

public class UIFader : MonoBehaviour
{
    public CanvasGroup uiGroup;
    public CanvasGroup backgroundGroup;

    public float duration = 0.3f;

    bool visible = false;

    public void ToggleUI()
    {
        StopAllCoroutines();

        if (visible)
            StartCoroutine(FadeOut());
        else
            StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        visible = true;

        float t = 0;

        uiGroup.blocksRaycasts = true;
        backgroundGroup.blocksRaycasts = true;

        while (t < duration)
        {
            t += Time.deltaTime;

            float a = t / duration;

            uiGroup.alpha = a;
            backgroundGroup.alpha = a * 1f; // fond un peu transparent

            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        visible = false;

        float t = 0;

        uiGroup.blocksRaycasts = false;
        backgroundGroup.blocksRaycasts = false;

        while (t < duration)
        {
            t += Time.deltaTime;

            float a = 1 - (t / duration);

            uiGroup.alpha = a;
            backgroundGroup.alpha = a * 0.8f;

            yield return null;
        }
    }
}
