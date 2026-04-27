using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the FlyPizza arc animation from the player to the house center.
/// Compensates for the scrolling speed of the ground so the pizza lands at the house.
/// </summary>
public class FlyPizzaLauncher : MonoBehaviour
{
    [SerializeField] private GameObject _FlyPizza;

    [Tooltip("Reference to one of the ground movement scripts to read the current scroll speed.")]
    [SerializeField] private GoundMouvement _GroundMouvement;

    [Tooltip("Arc height in world units.")]
    [SerializeField] private float _ArcHeight = 5f;

    [Tooltip("Duration of the flight in seconds.")]
    [SerializeField] private float _FlightDuration = 0.6f;

    private Coroutine _ActiveFlight;

    /// <summary>Launches the pizza from the player toward the house center.</summary>
    public void Launch(Transform houseTransform)
    {
        if (_ActiveFlight != null)
        {
            StopCoroutine(_ActiveFlight);
            _FlyPizza.SetActive(false);
        }
        _ActiveFlight = StartCoroutine(FlyToHouse(houseTransform));
    }

    private IEnumerator FlyToHouse(Transform houseTransform)
    {
        // Snap pizza to player position and activate
        _FlyPizza.transform.position = transform.position;
        _FlyPizza.SetActive(true);

        Vector2 startPos = _FlyPizza.transform.position;
        float elapsed = 0f;

        while (elapsed < _FlightDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _FlightDuration);

            // House is scrolling left; compute its predicted world position at end of flight
            float remainingTime = _FlightDuration - elapsed + Time.deltaTime;
            float scrollSpeed = _GroundMouvement != null ? _GroundMouvement.speed : 0f;

            // Target: house current center + expected scroll offset over the remaining flight
            Vector2 targetNow = new Vector2(
                houseTransform.position.x,
                houseTransform.position.y
            );

            // Bell-curve arc: height = 4h * t * (1-t)
            float arcY = 4f * _ArcHeight * t * (1f - t);

            // Lerp from start to the current house position (the house moves each frame,
            // so we target its live position to naturally compensate scrolling)
            Vector2 flatPos = Vector2.Lerp(startPos, targetNow, t);
            _FlyPizza.transform.position = new Vector3(flatPos.x, flatPos.y + arcY, _FlyPizza.transform.position.z);

            yield return null;
        }

        // Ensure the pizza lands exactly at house center then deactivate
        _FlyPizza.transform.position = new Vector3(
            houseTransform.position.x,
            houseTransform.position.y,
            _FlyPizza.transform.position.z
        );

        _FlyPizza.SetActive(false);
        _ActiveFlight = null;
    }
}
