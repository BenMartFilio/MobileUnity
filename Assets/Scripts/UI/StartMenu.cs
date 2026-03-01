using System.Collections;
using TMPro;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text playText;
    [SerializeField] private float fadeSpeed = 2f;
    [SerializeField] private InputPlayerManagerCustom m_inputManager;
    [SerializeField] private ChangeLevel level;
    private bool startActive = true;
    private float SizeOfText;

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
        SizeOfText = playText.fontSize;
        StartCoroutine(BlinkArrow(playText));
    }

    private IEnumerator BlinkArrow(TMP_Text playText)
    {
        float high = 1f;
        bool BiggerOut = true;

        while (startActive)
        {
            if (BiggerOut)
            {
                high -= Time.deltaTime * fadeSpeed;
                if (high <= 0.7f) // valeur minimale de l’opacité
                {
                    high = 0.7f;
                    BiggerOut = false;
                }
            }
            else
            {
                high += Time.deltaTime * fadeSpeed;
                if (high >= 1f)
                {
                    high = 1f;
                    BiggerOut = true;
                }
            }

            float c = SizeOfText*high;
            playText.fontSize = c;

            yield return null;
        }
    }

    private void StopArrow()
    {
        startActive = false;
        level.SwitchToLevel(1);
    }

}
