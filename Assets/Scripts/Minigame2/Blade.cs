using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;

    void OnEnable()
    {
        NewInputSystem2.OnSwipe += MoveBlade;
        NewInputSystem2.OnSwipeStart += StartBlade;
        NewInputSystem2.OnSwipeEnd += StopBlade;
    }

    void OnDisable()
    {
        NewInputSystem2.OnSwipe -= MoveBlade;
        NewInputSystem2.OnSwipeStart -= StartBlade;
        NewInputSystem2.OnSwipeEnd -= StopBlade;
    }

    void StartBlade(Vector2 pos)
    {
        trail.emitting = false;

        transform.position = pos;

        trail.Clear();
        trail.emitting = true;
    }

    void MoveBlade(Vector2 pos)
    {
        transform.position = pos;
    }

    void StopBlade()
    {
        trail.emitting = false;
    }
}