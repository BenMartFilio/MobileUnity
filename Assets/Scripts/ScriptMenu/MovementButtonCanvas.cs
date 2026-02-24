using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MovementButtonCanvas : MonoBehaviour
{
    [SerializeField] private Button[] miniGames;
    private Vector3 actualPosition;
    private Coroutine changingPosition = null;
    private bool _bAreWeStopped = true;

    void Start()
    {
        actualPosition = transform.position;
    }

    public void ChangePosition(Vector3 newPosition)
    {
        
        if (_bAreWeStopped == true && newPosition.y == 0)
        {
            JoinMiniGame();
        }
        else
        {
            if (changingPosition != null)
            {
                StopCoroutine(changingPosition);
            }

            actualPosition = transform.position;
            newPosition = actualPosition - newPosition;
            changingPosition = StartCoroutine(GoToPosition(newPosition));
        }
        
    }


    IEnumerator GoToPosition(Vector3 newPosition)
    {
        Vector3 startPosition = transform.position;
        float t = 0f;
        float duration = 0.4f;
        _bAreWeStopped = false;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(startPosition, newPosition, t);
            yield return null;
        }

        transform.position = newPosition;
        WhenInPosition();

    }

    public void WhenInPosition()
    {
        Debug.Log("WeStopped");
        _bAreWeStopped = true;
    }

    public void JoinMiniGame()
    {
        Debug.Log("JoinMiniGame");
    }
}
