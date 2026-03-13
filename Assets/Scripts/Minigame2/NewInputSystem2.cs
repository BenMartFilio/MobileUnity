using System;
using UnityEngine;

public class NewInputSystem2 : MonoBehaviour
{
    Vector2 lastPos;
    bool touching = false;
    [SerializeField] private LayerMask Gameplay;
    public static event Action<Vector2> OnSwipeStart;
    public static event Action<Vector2> OnSwipe;
    public static event Action OnSwipeEnd;
    void Update()
    {
        if (Input.touchCount == 0)
        {
            touching = false;
            OnSwipeEnd?.Invoke();
            return;
        }

        Touch touch = Input.GetTouch(0);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(touch.position);

        if (touch.phase == TouchPhase.Began)
            OnSwipeStart?.Invoke(worldPos);

        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            OnSwipe?.Invoke(worldPos);

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            OnSwipeEnd?.Invoke();

        if (!touching)
        {
            lastPos = worldPos;
            touching = true;
            return;
        }

        RaycastHit2D[] hits = Physics2D.CircleCastAll(lastPos, 0.1f, worldPos - lastPos, Vector2.Distance(lastPos, worldPos), Gameplay);

        foreach (var hit in hits)
        {
            Vector2 swipeDir = (worldPos - lastPos).normalized;

            hit.collider.GetComponent<SliceCollision>().OnExitCollision(swipeDir);
        }

        lastPos = worldPos;
    }
}
