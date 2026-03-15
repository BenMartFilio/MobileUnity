using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartTips2 : MonoBehaviour
{
    [SerializeField] private Image hand;
    [SerializeField] private float fadeSpeed = 1f;
    [SerializeField] private LastInputSystem3 m_inputManager;
    private bool tutorialActive = true;

    private void OnEnable()
    {
        m_inputManager.OnTapScreen += StopArrow;
    }
    private void OnDisable()
    {
        m_inputManager.OnTapScreen -= StopArrow;
    }

    private void Start()
    {
        StartCoroutine(BlinkArrow(hand));
    }

    private IEnumerator BlinkArrow(Image arrow)
    {
        float alpha = 1f;
        bool fadingOut = true;

        while (tutorialActive)
        {
            if (fadingOut)
            {
                alpha -= Time.deltaTime * fadeSpeed;
                if (alpha <= 0.3f) // valeur minimale de l’opacité
                {
                    alpha = 0.3f;
                    fadingOut = false;
                }
            }
            else
            {
                alpha += Time.deltaTime * fadeSpeed;
                if (alpha >= 1f)
                {
                    alpha = 1f;
                    fadingOut = true;
                }
            }

            Color c = arrow.color;
            c.a = alpha;
            arrow.color = c;

            yield return null;
        }
        arrow.gameObject.SetActive(false);
    }

    private void StopArrow()
    {
        tutorialActive = false;
        Destroy(hand);
        Destroy(gameObject);
    }
}
