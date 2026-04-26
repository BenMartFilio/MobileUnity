using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteAlways]
[RequireComponent(typeof(Image))]
public class AdaptOnScreen : MonoBehaviour
{
    private RectTransform rt;
    [SerializeField] private RectTransform[] ReferencesToPoint;

    void OnEnable()
    {
        rt = GetComponent<RectTransform>();
        StartCoroutine(StretchNextFrame());
    }

    IEnumerator StretchNextFrame()
    {
        yield return new WaitForEndOfFrame();
        Stretch();
    }

    void Stretch()
    {
        Camera cam = Camera.main;
        if (cam == null || rt == null) return;

        // Calcul de la largeur monde via la caméra (comme le script original)
        float worldHeight = cam.orthographicSize * 2f;
        float worldWidth = worldHeight * cam.aspect;

        // En paysage, on swap si nécessaire
        if (Screen.width < Screen.height)
            worldWidth = worldHeight / cam.aspect;

        Debug.Log($"[AdaptOnScreen] worldWidth = {worldWidth}, worldHeight = {worldHeight}");

        // Redimensionne le RectTransform aux dimensions monde
        rt.sizeDelta = new Vector2(worldWidth, rt.sizeDelta.y);
     //   rt.anchoredPosition = Vector2.zero;

        if (ReferencesToPoint == null || ReferencesToPoint.Length == 0) return;

        float scaleRoad = worldWidth / 3f;

        for (int i = 0; i < ReferencesToPoint.Length; i++)
        {
            if (ReferencesToPoint[i] == null) continue;
            Vector2 pos = ReferencesToPoint[i].anchoredPosition;
            pos.x = scaleRoad * (i + 1) - (worldWidth - scaleRoad);
            ReferencesToPoint[i].anchoredPosition = pos;
        }
    }
}