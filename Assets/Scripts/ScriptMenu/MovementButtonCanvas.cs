using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MovementButtonCanvas : MonoBehaviour
{
    private Vector3 actualPosition;
    private Coroutine changingPosition = null;
    private bool _bAreWeStopped = true;
    [SerializeField] private TMP_Text textToChange;
    [SerializeField] private GetPositionAndCallCanvas[] boutonToGet;
    [SerializeField] private string[] listOfMiniGame;
    [SerializeField] private int[] listOfSceneMiniGame;
    [SerializeField] private ChangeLevel levelManager;
    private int IDOfButton = 0;

    void Start()
    {
        actualPosition = transform.position;
        WhenInPosition();
    }

    public void IDButton(int ID)
    {
        boutonToGet[ID].ChangerPos();
        IDOfButton = ID;
    }

    public void ChangePosition(Vector3 newPosition)
    {
        
        if (_bAreWeStopped == true && newPosition.y == 0)
        {
            JoinMiniGame(IDOfButton);
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
        _bAreWeStopped = true;
        textToChange.text = listOfMiniGame[IDOfButton];
    }

    public void JoinMiniGame(int ID)
    {
        levelManager.SwitchToLevel(listOfSceneMiniGame[ID]);
    }
}
