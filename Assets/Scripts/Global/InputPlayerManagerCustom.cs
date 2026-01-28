using System;
using UnityEngine;

public class InputPlayerManagerCustom : MonoBehaviour
{
    public event Action OnMoveLeft;
    public event Action OnMoveRight;
    [SerializeField] private float _tapDuration = 1.0f;
    private float _tapTimer = 0.0f;
    private bool _isTouching = false;
    private float width = 0.0f;
    private float height = 0.0f;

    private void Start()
    {
        width = Screen.width;
        height = Screen.height;
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0);

            if (firstTouch.phase == TouchPhase.Began)
            {
                _isTouching = true;
            }
            else if (firstTouch.phase == TouchPhase.Ended)
            {
                _isTouching = false;
                if (_tapTimer <= _tapDuration)
                {
                    Debug.LogWarning($"Tap OK !! Touch at {firstTouch.position}");
                    if(firstTouch.position.x > width / 2)
                    {
                        Debug.Log("Tap Right !!");
                    }
                    else
                    {
                        Debug.Log("Tap Left !!");
                    }
                }
                _tapTimer = 0.0f;
            }



            if (_isTouching)
            {
                _tapTimer += Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }
        }
    }


    public void MoveLeft()
    {
        OnMoveLeft?.Invoke();
    }
    public void MoveRight()
    {
        OnMoveRight?.Invoke();
    }
}
