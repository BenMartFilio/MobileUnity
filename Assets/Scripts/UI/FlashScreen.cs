using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashScreen : MonoBehaviour
{
    [SerializeField] private Image flashImage;
    [SerializeField] private float flashDuration = 0.15f;
    [SerializeField] private float maxAlpha = 0.5f;

    private Coroutine flashCoroutine;

    public void TriggerFlash()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        float timer = 0f;

        while (timer < flashDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0, maxAlpha, timer / flashDuration);
            SetAlpha(alpha);
            yield return null;
        }

        timer = 0f;

        while (timer < flashDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(maxAlpha, 0, timer / flashDuration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(0);
    }

    private void SetAlpha(float alpha)
    {
        Color c = flashImage.color;
        c.a = alpha;
        flashImage.color = c;
    }
}