using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class InputPlayerManagerCustom : MonoBehaviour
{
    public event Action OnMoveLeft;
    public event Action OnMoveRight;
    [SerializeField] private float _tapDuration = 1.0f;
    private float _tapTimer = 0.0f;
    private bool _isTouching = false;
    private float width = 0.0f;
    private float height = 0.0f;
    private float _firstPosition;
    private float _endPosition;


   /* InputAction _tapAction;*/

    private void Start()
    {
        width = Screen.width;
        height = Screen.height;

        /*_tapAction = InputSystem.actions.FindAction("Tap");*/
    }

   /*public void OnTap()
    {
        Debug.Log("Tap");
    }*/

    private void Update()
    {
        /*if (Touch.activeTouches.Count <= 0)
        {
            return;
        }*/

        /*Touch touch = _tapAction.ReadValue<Touch>();
        if (touch.phase == UnityEngine.TouchPhase.Began)
        {
        }*/

        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0);

            if (firstTouch.phase == TouchPhase.Began)  //Récupérer l'information de began, puis celle d'end pour le swipe
            {
                _isTouching = true;
                _firstPosition = firstTouch.position.x;
            }
            else if (firstTouch.phase == TouchPhase.Ended)
            {
                _isTouching = false;
                _endPosition = firstTouch.position.x;
                if (_tapTimer <= _tapDuration)
                {
                    Debug.LogWarning($"Tap OK !! Touch at {firstTouch.position}");
                    if (firstTouch.position.x > width / 2)
                    {
                        Debug.Log("Tap Right !!");
                    }
                    else
                    {
                        Debug.Log("Tap Left !!");
                    }
                }
                else if (_tapTimer >= _tapDuration) 
                {
                    if (_endPosition - _firstPosition >= 100)
                    {
                        Debug.Log("Swipe Direction 1");
                    }
                    else if (_endPosition - _firstPosition <= -100)
                    {
                        Debug.Log("Swipe Direction 2");
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
