using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class InputPlayerManagerCustom : MonoBehaviour
{
    public event Action OnMoveLeft;
    public event Action OnMoveRight;
    public event Action OnTapScreen;
    [SerializeField] private float _tapDuration = 0.5f;
    private float _tapTimer = 0.0f;
    private bool _isTouching = false;
    private float width = 0.0f;
    private float height = 0.0f;
    private float _firstPosition;
    private float _endPosition;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered




    /* InputAction _tapAction;*/

    private void Start()
    {
        width = Screen.width;
        height = Screen.height;

        dragDistance = Screen.height * 15 / 100;

        /*_tapAction = InputSystem.actions.FindAction("Tap");*/
    }

   /*public void OnTap()
    {
        Debug.Log("Tap");
    }*/

    private void Update()
    {

        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            MoveRight();
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            MoveLeft();
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                    OnTapScreen?.Invoke();
                }
            }
        }

        /*if (Touch.activeTouches.Count <= 0)
        {
            return;
        }*/

        /*Touch touch = _tapAction.ReadValue<Touch>();
        if (touch.phase == UnityEngine.TouchPhase.Began)
        {
        }*/

        /*  if (Input.touchCount > 0)
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
                       else
                       {
                           Debug.Log(_endPosition - _firstPosition);
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
           }*/
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
